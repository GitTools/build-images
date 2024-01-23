namespace Build;

public abstract class DockerBaseTask : FrostingTask<BuildContext>
{
    protected virtual void DockerImage(BuildContext context, DockerDepsImage dockerImage)
    {
        var buildSettings = GetBuildSettings(dockerImage, context.DockerRegistry);

        context.DockerBuildXBuild(buildSettings, GetWorkingDir(dockerImage).ToString());

        var dockerTags = GetDockerTags(dockerImage, context.DockerRegistry, dockerImage.Architecture).ToArray();

        if (!context.PushImages)
            return;

        foreach (var tag in dockerTags)
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
            context.DockerManifestCreate(tag, amd64Tag, arm64Tag);

            if (context.PushImages)
            {
                context.DockerManifestPush(tag);
            }
            context.DockerManifestRemove(tag);
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
            Output = ["type=docker"],
            Pull = true,
            NoCache = true,
            Label =
            [
                "maintainers=GitTools Maintainers",
                "org.opencontainers.image.authors=GitTools Maintainers",
                "org.opencontainers.image.licenses=MIT",
                "org.opencontainers.image.source=https://github.com/GitTools/build-images.git"
            ],
        };
        return buildSettings;
    }

    protected abstract DirectoryPath GetWorkingDir(DockerDepsImage dockerImage);
    protected abstract IEnumerable<string> GetDockerTags(DockerDepsImage dockerImage, string dockerRegistry, Architecture? arch = null);
}
