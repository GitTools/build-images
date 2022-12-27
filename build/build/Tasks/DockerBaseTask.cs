﻿public abstract class DockerBaseTask : FrostingTask<BuildContext>
{
    protected virtual void DockerImage(BuildContext context, DockerDepsImage dockerImage)
    {
        var buildSettings = GetBuildSettings(dockerImage, context.DockerRegistry);

        context.DockerBuild(buildSettings, GetWorkingDir(dockerImage).ToString(), "--output type=docker");

        var dockerTags = GetDockerTags(dockerImage, context.DockerRegistry, dockerImage.Architecture).ToArray();

        if (!context.PushImages)
            return;

        foreach (var tag in dockerTags)
        {
            context.DockerPush(tag);
        }
    }

    protected void DockerManifest(BuildContext context, DockerDepsImage dockerImage, bool skipArm64Image = false)
    {
        var manifestTags = GetDockerTags(dockerImage, context.DockerRegistry);
        foreach (var tag in manifestTags)
        {
            var amd64Tag = $"{tag}-{Architecture.Amd64.ToSuffix()}";
            if (skipArm64Image)
            {
                context.DockerManifestCreate(tag, amd64Tag);
            }
            else
            {
                var arm64Tag = $"{tag}-{Architecture.Arm64.ToSuffix()}";
                context.DockerManifestCreate(tag, amd64Tag, arm64Tag);
            }

            if (context.PushImages)
            {
                context.DockerManifestPush(tag);
            }
            context.DockerManifestRemove(tag);
        }
    }

    protected virtual DockerImageBuildSettings GetBuildSettings(DockerDepsImage dockerImage, string registry)
    {
        var arch = dockerImage.Architecture;
        var dockerTags = GetDockerTags(dockerImage, registry, arch).ToArray();
        var buildSettings = new DockerImageBuildSettings
        {
            Rm = true,
            Tag = dockerTags,
            Platform = string.Join(",", $"linux/{arch.ToString().ToLower()}"),
            Pull = true,
        };
        return buildSettings;
    }

    protected abstract DirectoryPath GetWorkingDir(DockerDepsImage dockerImage);
    protected abstract IEnumerable<string> GetDockerTags(DockerDepsImage dockerImage, string dockerRegistry, Architecture? arch = null);
}
