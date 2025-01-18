using DockerBuildXBuildSettings = Build.Cake.Docker.DockerBuildXBuildSettings;
using DockerBuildXImageToolsCreateSettings = Build.Cake.Docker.DockerBuildXImageToolsCreateSettings;

namespace Build;

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
        foreach (var group in context.Images.GroupBy(x => new { x.Distro, x.Variant, x.Version }))
        {
            var dockerImage = group.First();
            DockerManifest(context, dockerImage);
        }
    }

    protected override void DockerImage(BuildContext context, DockerDepsImage dockerImage)
    {
        GenerateDockerfile(context, GetWorkingDir(dockerImage), dockerImage);
        base.DockerImage(context, dockerImage);
    }

    protected override DirectoryPath GetWorkingDir(DockerDepsImage dockerImage) =>
        DirectoryPath.FromString("./src/linux");

    protected override DockerBuildXBuildSettings GetBuildSettings(DockerDepsImage dockerImage, string registry)
    {
        var buildSettings = base.GetBuildSettings(dockerImage, registry);

        var workDir = GetWorkingDir(dockerImage);

        var image = dockerImage as DockerImage;
        var (distro, version, variant, arch) = (DockerImage)dockerImage;
        buildSettings.File = $"{workDir}/Dockerfile.build";
        buildSettings.BuildArg = [$"DOTNET_VERSION={image!.Version}", $"TAG={image.Distro}"];

        var suffix = $"({distro}-{variant}-{version}-{arch.ToSuffix()})";
        buildSettings.Label =
        [
            .. buildSettings.Label,
            $"{Prefix}.description=GitTools build images {suffix}"
        ];
        buildSettings.Annotation =
        [
            .. buildSettings.Annotation,
            $"{Prefix}.description=GitTools build images {suffix}"
        ];
        return buildSettings;
    }

    protected override DockerBuildXImageToolsCreateSettings GetManifestSettings(DockerDepsImage dockerImage, string tag)
    {
        var (distro, version, variant, _) = (DockerImage)dockerImage;
        var suffix = $"({distro}-{variant}-{version})";
        var settings = base.GetManifestSettings(dockerImage, tag);
        settings.Annotation =
        [
            .. settings.Annotation,
            $"index:{Prefix}.description=GitTools build images {suffix}",
        ];
        return settings;
    }

    protected override IEnumerable<string> GetDockerTags(DockerDepsImage dockerImage, string dockerRegistry,
        Architecture? arch = null)
    {
        var (distro, version, variant, _) = (dockerImage as DockerImage)!;

        var tags = new List<string>
        {
            $"{dockerRegistry}/{Constants.DockerImageName}:{distro}-{variant}-{version}"
        };

        if (version == Constants.DockerDistroLatest)
        {
            tags.AddRange(
            [
                $"{dockerRegistry}/{Constants.DockerImageName}:{distro}-{variant}-latest"
            ]);
        }

        if (!arch.HasValue) return tags;

        var suffix = arch.Value.ToSuffix();
        return tags.Select(x => $"{x}-{suffix}");
    }

    private static void GenerateDockerfile(ICakeContext context, DirectoryPath? workDir, DockerDepsImage dockerImage)
    {
        var variant = ((DockerImage)dockerImage).Variant;

        const string installScript =
            """
            # install dotnet
            RUN wget https://dot.net/v1/dotnet-install.sh -O $HOME/dotnet-install.sh --no-check-certificate\
            && chmod +x $HOME/dotnet-install.sh \
            && $HOME/dotnet-install.sh --channel $DOTNET_VERSION --install-dir /usr/local/bin
            """;

        var dockerfile = $"{workDir}/Dockerfile";
        var content = new StringBuilder(context.FileReadText(dockerfile));

        content.AppendLine();

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
