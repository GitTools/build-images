using DockerBuildXBuildSettings = Build.Cake.Docker.DockerBuildXBuildSettings;
using DockerBuildXImageToolsCreateSettings = Build.Cake.Docker.DockerBuildXImageToolsCreateSettings;

namespace Build;

public abstract class DockerBaseTask : FrostingTask<BuildContext>
{
    private static readonly string[] Annotations =
    [
        "org.opencontainers.image.authors=GitTools Maintainers",
        "org.opencontainers.image.vendor=GitTools",
        "org.opencontainers.image.licenses=MIT",
        "org.opencontainers.image.source=https://github.com/GitTools/build-images.git",
        $"org.opencontainers.image.created={DateTime.UtcNow:O}",
    ];

    protected virtual void DockerImage(BuildContext context, DockerDepsImage dockerImage)
    {
        var buildSettings = GetBuildSettings(dockerImage, context.DockerRegistry);

        var path = GetWorkingDir(dockerImage).ToString();
        context.DockerBuildXBuild(buildSettings, path);

        if (!context.PushImages)
            return;

        foreach (var tag in buildSettings.Tag)
        {
            context.DockerPush(tag);
        }
    }

    protected void DockerManifest(BuildContext context, DockerDepsImage dockerImage)
    {
        var manifestTags = GetDockerTags(dockerImage, context.DockerRegistry);
        foreach (var tag in manifestTags)
        {
            var amd64Tag = $"{tag}-{Architecture.Amd64.ToSuffix()}";
            var arm64Tag = $"{tag}-{Architecture.Arm64.ToSuffix()}";

            var settings = new DockerBuildXImageToolsCreateSettings
            {
                Tag = [tag],
                Annotation =
                [
                    .. Annotations.Select(a => "index:" + a).ToArray(),
                ]
            };
            context.DockerBuildXImageToolsCreate(settings, [amd64Tag, arm64Tag]);
        }
    }

    protected virtual DockerBuildXBuildSettings GetBuildSettings(DockerDepsImage dockerImage, string registry)
    {
        var arch = dockerImage.Architecture;
        var suffix = arch.ToSuffix();
        var dockerTags = GetDockerTags(dockerImage, registry, arch).ToArray();
        var buildSettings = new DockerBuildXBuildSettings
        {
            Rm = true,
            Tag = dockerTags,
            Platform = [$"linux/{suffix}"],
            Output = ["type=docker,oci-mediatypes=true"],
            Pull = true,
            // NoCache = true,
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

    protected abstract IEnumerable<string> GetDockerTags(DockerDepsImage dockerImage, string dockerRegistry,
        Architecture? arch = null);
}
