using DockerBuildXBuildSettings = Build.Cake.Docker.DockerBuildXBuildSettings;

namespace Build;

[TaskName(nameof(DockerBuildDeps))]
[TaskDescription("Builds the docker images dependencies")]
public sealed class DockerBuildDeps : BaseDockerBuild
{
    public override void Run(BuildContext context)
    {
        // build/push images
        foreach (var dockerImage in context.DepsImages)
        {
            DockerImage(context, dockerImage);
        }
    }

    protected override DirectoryPath GetWorkingDir(DockerDepsImage dockerImage) =>
        DirectoryPath.FromString($"./src/linux/{dockerImage.Distro}");

    protected override DockerBuildXBuildSettings GetBuildSettings(DockerDepsImage dockerImage, string registry)
    {
        var buildSettings = base.GetBuildSettings(dockerImage, registry);
        var (distro, arch) = dockerImage;

        var workDir = GetWorkingDir(dockerImage);
        buildSettings.File = $"{workDir}/Dockerfile";
        buildSettings.Label =
        [
            .. buildSettings.Label,
            $"{Prefix}.description=GitTools deps images ({distro}-{arch.ToSuffix()})"
        ];
        buildSettings.Annotation =
        [
            .. buildSettings.Annotation,
            $"{Prefix}.description=GitTools deps images ({distro}-{arch.ToSuffix()})"
        ];

        return buildSettings;
    }
}
