using System.Linq;
using Cake.Docker;
using Cake.Frosting;

[TaskName(nameof(DockerPushDeps))]
[TaskDescription("Pushes the docker deps to the repository")]
[IsDependentOn(typeof(DockerBuildDeps))]
public sealed class DockerPushDeps : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (var dockerImage in context.Images.GroupBy(x => x.Distro))
        {
            PushDockerImage(context, dockerImage.Key);
        }
    }

    private static void PushDockerImage(BuildContext context, string distro)
    {
        var dockerTags = new[]
        {
            $"{context.DockerRepository}/{Constants.DockerImageDeps}:{distro}"
        };

        foreach (var tag in dockerTags)
        {
            context.DockerPush(tag);
        }
    }
}
