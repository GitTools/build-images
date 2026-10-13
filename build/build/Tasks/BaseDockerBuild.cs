using DockerBuildXBuildSettings = Build.Cake.Docker.DockerBuildXBuildSettings;

namespace Build;

public abstract class BaseDockerBuild : FrostingTask<BuildContext>
{
    protected const string Prefix = "org.opencontainers.image";

    private static readonly string[] Annotations =
    [
        $"{Prefix}.authors=GitTools Maintainers",
        $"{Prefix}.vendor=GitTools",
        $"{Prefix}.licenses=MIT",
        $"{Prefix}.source=https://github.com/GitTools/build-images.git",
        $"{Prefix}.created={DateTime.UtcNow:O}",
    ];

    protected virtual void DockerImage(BuildContext context, DockerDepsImage dockerImage)
    {
        var buildSettings = GetBuildSettings(dockerImage, context.DockerRegistry);

        var path = GetWorkingDir(dockerImage).ToString();
        context.DockerBuildXBuild(buildSettings, path);

        // Validate the image's default user before any tag can be published.
        var identity = context.DockerRun(new DockerContainerRunSettings
        {
            Rm = true,
            Entrypoint = "id",
            Platform = buildSettings.Platform.Single()
        }, buildSettings.Tag.First(), "-u");
        if (string.Join("", identity).Trim() != "10001")
        {
            throw new CakeException("GitTools images must run as non-root UID 10001 by default.");
        }

        if (!context.PushImages)
            return;

        foreach (var tag in buildSettings.Tag)
        {
            context.DockerPush(tag);
        }
    }

    protected virtual DockerBuildXBuildSettings GetBuildSettings(DockerDepsImage dockerImage, string registry)
    {
        var arch = dockerImage.Architecture;
        var suffix = arch.ToSuffix();
        var dockerTags = dockerImage.GetDockerTags(registry, arch).ToArray();
        var buildSettings = new DockerBuildXBuildSettings
        {
            Rm = true,
            Pull = true,
            // NoCache = true,
            Tag = dockerTags,
            Platform = [$"linux/{suffix}"],
            Output = ["type=docker,oci-mediatypes=true"],
            Label =
            [
                "maintainers=GitTools Maintainers",
                .. Annotations,
            ],
            Annotation = Annotations
        };
        return buildSettings;
    }

    protected abstract DirectoryPath GetWorkingDir(DockerDepsImage dockerImage);
}
