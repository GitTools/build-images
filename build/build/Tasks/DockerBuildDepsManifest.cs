namespace Build;
using DockerBuildXImageToolsCreateSettings = Build.Cake.Docker.DockerBuildXImageToolsCreateSettings;

[TaskName(nameof(DockerBuildDepsManifest))]
[TaskDescription("Builds the docker images dependencies manifest")]
public class DockerBuildDepsManifest : BaseDockerBuildManifest
{
    public override void Run(BuildContext context)
    {
        if (!context.PushImages)
            return;

        // build/push manifests
        foreach (var group in context.DepsImages.GroupBy(x => new { x.Distro }))
        {
            var dockerImage = group.First();
            DockerManifest(context, dockerImage);
        }
    }

    protected override DockerBuildXImageToolsCreateSettings GetManifestSettings(DockerDepsImage dockerImage, string tag)
    {
        var settings = base.GetManifestSettings(dockerImage, tag);
        settings.Annotation =
        [
            .. settings.Annotation,
            $"index:{Prefix}.description=GitTools deps images ({dockerImage.Distro})"
        ];
        return settings;
    }
}
