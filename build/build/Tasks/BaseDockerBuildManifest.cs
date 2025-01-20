namespace Build;
using DockerBuildXImageToolsCreateSettings = Build.Cake.Docker.DockerBuildXImageToolsCreateSettings;

public abstract class BaseDockerBuildManifest : FrostingTask<BuildContext>
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

    protected void DockerManifest(BuildContext context, DockerDepsImage dockerImage)
    {
        var manifestTags = dockerImage.GetDockerTags(context.DockerRegistry);
        foreach (var tag in manifestTags)
        {
            var amd64Tag = $"{tag}-{Architecture.Amd64.ToSuffix()}";
            var arm64Tag = $"{tag}-{Architecture.Arm64.ToSuffix()}";

            var settings = GetManifestSettings(dockerImage, tag);
            context.DockerBuildXImageToolsCreate(settings, [amd64Tag, arm64Tag]);
        }
    }

    protected virtual DockerBuildXImageToolsCreateSettings GetManifestSettings(DockerDepsImage dockerImage, string tag)
    {
        var settings = new DockerBuildXImageToolsCreateSettings
        {
            Tag = [tag],
            Annotation =
            [
                .. Annotations.Select(a => "index:" + a).ToArray(),
            ]
        };
        return settings;
    }
}
