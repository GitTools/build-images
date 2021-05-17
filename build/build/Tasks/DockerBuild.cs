using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Docker;
using Cake.FileHelpers;
using Cake.Frosting;

[TaskName(nameof(DockerBuild))]
[TaskDescription("Builds the docker images")]
public sealed class DockerBuild : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (var dockerImage in context.Images)
        {
            BuildDockerImage(context, dockerImage);
        }
    }

    private static void BuildDockerImage(ICakeContext context, DockerImage dockerImage)
    {
        var (distro, version, variant) = dockerImage;
        var workDir = DirectoryPath.FromString($"./src/linux/{distro}");

        var dockerhubTags = dockerImage.GetDockerTagsForRepository(Constants.DockerHubRegistry);
        var githubTags = dockerImage.GetDockerTagsForRepository(Constants.GitHubContainerRegistry);

        var dockerfile = $"{workDir}/Dockerfile";
        var content = context.FileReadText(dockerfile);
        if (variant == "sdk")
        {
            content += "\nRUN dotnet tool install powershell --global";
            if (version == "3.1")
            {
                content += " --version 7.0.3";
            }
            content += "\nRUN ln -sf /root/.dotnet/tools/pwsh /usr/bin/pwsh";
        }

        context.FileWriteText($"{workDir}/Dockerfile.build", content);

        var buildSettings = new DockerImageBuildSettings
        {
            Rm = true,
            Tag = dockerhubTags.Union(githubTags).ToArray(),
            File = $"{workDir}/Dockerfile.build",
            BuildArg = new[] { $"DOTNET_VERSION={version}", $"DOTNET_VARIANT={variant}" },
            Pull = true,
        };

        context.DockerBuild(buildSettings, workDir.ToString());
    }
}
