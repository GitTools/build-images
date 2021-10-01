[TaskName(nameof(DockerBuildDeps))]
[TaskDescription("Builds the docker images dependencies")]
public sealed class DockerBuildDeps : DockerBaseTask
{
    public override void Run(BuildContext context)
    {
        // build/push images
        foreach (var dockerImage in context.DepsImages)
        {
            DockerImage(context, dockerImage);
        }

        // build/push manifests
        foreach (var group in context.DepsImages.GroupBy(x => new { x.Distro }))
        {
            var dockerImage = group.First();
            DockerManifest(context, dockerImage);
        }
    }

    protected override DirectoryPath GetWorkingDir(DockerDepsImage dockerImage) => DirectoryPath.FromString($"./src/linux/{dockerImage.Distro}");

    protected override DockerImageBuildSettings GetBuildSettings(DockerDepsImage dockerImage, string registry)
    {
        var buildSettings = base.GetBuildSettings(dockerImage, registry);

        var workDir = GetWorkingDir(dockerImage);
        buildSettings.File = $"{workDir}/Dockerfile";

        return buildSettings;
    }

    protected override IEnumerable<string> GetDockerTags(DockerDepsImage dockerImage, string dockerRegistry, Architecture? arch = null)
    {
        var tags = new[]
        {
            $"{dockerRegistry}/{Constants.DockerImageDeps}:{dockerImage.Distro}",
        };

        if (!arch.HasValue) return tags;

        var suffix = arch.Value.ToSuffix();
        return tags.Select(x => $"{x}-{suffix}");
    }
}
