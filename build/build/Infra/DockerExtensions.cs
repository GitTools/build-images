namespace Build;

public static class DockerExtensions
{
    public static void DockerBuildXBuild(this ICakeContext context, DockerBuildXBuildSettings settings, string path,
        params string[] args)
    {
        var runner = new GenericDockerRunner<DockerBuildXBuildSettings>(context.FileSystem, context.Environment,
            context.ProcessRunner, context.Tools);

        path = $"\"{path.Trim().Trim('\"')}\"";
        runner.Run("buildx build", settings, [.. args, path]);
    }

    public static void DockerManifestRemove(this ICakeContext context, string tag) => context.DockerCustomCommand($"manifest rm {tag}");
}
