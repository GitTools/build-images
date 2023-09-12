[TaskName(nameof(DockerBuild))]
[TaskDescription("Builds the docker images")]
public sealed class DockerBuild : DockerBaseTask
{
    public override void Run(BuildContext context)
    {
        // build/push images
        foreach (var dockerImage in context.Images)
        {
            DockerImage(context, dockerImage);
        }

        if (!context.PushImages)
            return;

        // build/push manifests
        foreach (var group in context.Images.GroupBy(x => new { x.Distro, x.Variant, x.Version}))
        {
            var amd64DockerImage = group.First(x => x.Architecture == Architecture.Amd64);
            var arm64DockerImage = group.First(x => x.Architecture == Architecture.Arm64);
            DockerManifest(context, amd64DockerImage);
        }
    }

    protected override void DockerImage(BuildContext context, DockerDepsImage dockerImage)
    {
        GenerateDockerfile(context, GetWorkingDir(dockerImage), (dockerImage as DockerImage)!.Variant);
        base.DockerImage(context, dockerImage);
    }

    protected override DirectoryPath GetWorkingDir(DockerDepsImage dockerImage) => DirectoryPath.FromString("./src/linux");

    protected override DockerImageBuildSettings GetBuildSettings(DockerDepsImage dockerImage, string registry)
    {
        var buildSettings = base.GetBuildSettings(dockerImage, registry);

        var workDir = GetWorkingDir(dockerImage);

        var image = dockerImage as DockerImage;
        buildSettings.File = $"{workDir}/Dockerfile.build";
        buildSettings.BuildArg = new[] { $"DOTNET_VERSION={image!.Version}", $"TAG={image.Distro}" };

        return buildSettings;
    }

    protected override IEnumerable<string> GetDockerTags(DockerDepsImage dockerImage, string dockerRegistry, Architecture? arch = null)
    {
        var (distro, version, variant, _) = (dockerImage as DockerImage)!;

        var tags = new List<string> {
            $"{dockerRegistry}/{Constants.DockerImageName}:{distro}-{variant}-{version}",
        };

        if (version == Constants.VersionForDockerLatest) {
            tags.AddRange(new[] {
                $"{dockerRegistry}/{Constants.DockerImageName}:{distro}-{variant}-latest",
            });
        }

        if (!arch.HasValue) return tags;

        var suffix = arch.Value.ToSuffix();
        return tags.Select(x => $"{x}-{suffix}");
    }
    private static void GenerateDockerfile(ICakeContext context, DirectoryPath? workDir, string? variant)
    {
        const string installScript = @"
# install dotnet
RUN wget https://dot.net/v1/dotnet-install.sh -O $HOME/dotnet-install.sh --no-check-certificate\
    && chmod +x $HOME/dotnet-install.sh \
    && $HOME/dotnet-install.sh --channel $DOTNET_VERSION --install-dir /usr/local/bin";

        var dockerfile = $"{workDir}/Dockerfile";
        var content = new StringBuilder(context.FileReadText(dockerfile));

        content.Append(installScript);
        if (variant == "runtime")
        {
            content.Append(" --runtime dotnet");
        }
        content.AppendLine();

        var filePath = $"{workDir}/Dockerfile.build";
        context.FileWriteText(filePath, content.ToString());
    }
}
