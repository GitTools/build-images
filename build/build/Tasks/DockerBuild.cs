using System.Linq;
using System.Text;
using Cake.Core;
using Cake.Core.IO;
using Cake.Docker;
using Cake.FileHelpers;
using Cake.Frosting;

[TaskName(nameof(DockerBuild))]
[TaskDescription("Builds the docker images")]
public sealed class DockerBuild : FrostingTask<BuildContext>
{
    private const string InstallScript = @"
# install dotnet
RUN wget https://dot.net/v1/dotnet-install.sh -O $HOME/dotnet-install.sh \
    && chmod +x $HOME/dotnet-install.sh \
    && $HOME/dotnet-install.sh --channel $DOTNET_VERSION --install-dir /usr/local/bin";
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
        var workDir = DirectoryPath.FromString($"./src/linux");

        var dockerhubTags = dockerImage.GetDockerTagsForRepository(Constants.DockerHubRegistry);
        var githubTags = dockerImage.GetDockerTagsForRepository(Constants.GitHubContainerRegistry);

        var dockerfile = $"{workDir}/Dockerfile";
        var content = new StringBuilder(context.FileReadText(dockerfile));

        content.AppendLine();
        content.Append(InstallScript);
        if (variant == "runtime")
        {
            content.Append(" --runtime dotnet");
        }
        content.AppendLine();
        content.AppendLine();

        if (variant == "sdk")
        {
            content.AppendLine("RUN dotnet tool install powershell --global");
            if (version == "3.1")
                content.Append(" --version 7.0.3");

            content.AppendLine("RUN ln -sf /root/.dotnet/tools/pwsh /usr/bin/pwsh");
            content.AppendLine();
        }
        content.AppendLine("WORKDIR /app");

        context.FileWriteText($"{workDir}/Dockerfile.build", content.ToString());

        var buildSettings = new DockerImageBuildSettings
        {
            Rm = true,
            Tag = dockerhubTags.Union(githubTags).ToArray(),
            File = $"{workDir}/Dockerfile.build",
            BuildArg = new[] { $"DOTNET_VERSION={version}", $"TAG={distro}", $"BASE_IMAGE={Constants.GitHubContainerRegistry}/{Constants.DockerImageDeps}" },
            Platform = "linux/amd64",
            Pull = true,
        };

        context.DockerBuild(buildSettings, workDir.ToString());
    }
}
