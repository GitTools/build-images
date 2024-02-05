namespace Build;
using DockerBuildXBuildSettings = Build.Cake.Docker.DockerBuildXBuildSettings;
using DockerBuildXImageToolsCreateSettings = Build.Cake.Docker.DockerBuildXImageToolsCreateSettings;

public static class DockerExtensions
{
    public static void DockerBuildXBuild(this ICakeContext context, DockerBuildXBuildSettings settings, DirectoryPath target)
    {
        ArgumentNullException.ThrowIfNull(context);
        var runner = new GenericDockerRunner<DockerBuildXBuildSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run("buildx build", settings, [target.ToString().EscapeProcessArgument()]);
    }

    public static void DockerBuildXImageToolsCreate(this ICakeContext context, DockerBuildXImageToolsCreateSettings settings, IEnumerable<string>? target = null)
    {
        ArgumentNullException.ThrowIfNull(context);
        var runner = new GenericDockerRunner<DockerBuildXImageToolsCreateSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run("buildx imagetools create", settings, target?.ToArray() ?? []);
    }
}
