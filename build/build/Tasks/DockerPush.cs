using Cake.Core;
using Cake.Docker;
using Cake.Frosting;

[TaskName(nameof(DockerPush))]
[TaskDescription("Pushes the docker images to the repository")]
[IsDependentOn(typeof(DockerBuild))]
public sealed class DockerPush : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (var dockerImage in context.Images)
        {
            PushDockerImage(context, dockerImage);
        }
    }

    private static void PushDockerImage(ICakeContext context, DockerImage dockerImage)
    {
        var tags = dockerImage.GetDockerTagsForRepository(Constants.DockerHubPrefix);

        foreach (var tag in tags)
        {
            context.DockerPush(tag);
        }
    }
}
