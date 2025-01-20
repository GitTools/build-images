namespace Build;
using DockerBuildXImageToolsCreateSettings = Build.Cake.Docker.DockerBuildXImageToolsCreateSettings;

[TaskName(nameof(DockerBuildImagesManifest))]
[TaskDescription("Builds the docker images manifest")]
public class DockerBuildImagesManifest : BaseDockerBuildManifest
{
    public override void Run(BuildContext context)
    {
        if (!context.PushImages)
            return;

        // build/push manifests
        foreach (var group in context.Images.GroupBy(x => new { x.Distro, x.Variant, x.Version }))
        {
            var dockerImage = group.First();
            DockerManifest(context, dockerImage);
        }
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
}
