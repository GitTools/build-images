using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Docker;
using Cake.Frosting;

[TaskName(nameof(DockerBuildDeps))]
[TaskDescription("Builds the docker images dependencies")]
public sealed class DockerBuildDeps : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (var dockerImage in context.Images.GroupBy(x => x.Distro))
        {
            BuildDockerImage(context, dockerImage.Key);
        }
    }

    private static void BuildDockerImage(ICakeContext context, string distro)
    {
        var workDir = DirectoryPath.FromString($"./src/linux/{distro}");

        var dockerTags = new[]
        {
            $"{Constants.DockerHubRegistry}/{Constants.DockerImageDeps}:{distro}",
        };

        var buildSettings = new DockerImageBuildSettings
        {
            Rm = true,
            Tag = dockerTags.ToArray(),
            File = $"{workDir}/Dockerfile",
            Platform = "linux/amd64,linux/arm64",
            Pull = true,
        };

        context.DockerBuild(buildSettings, workDir.ToString(), "--push");
    }
}
