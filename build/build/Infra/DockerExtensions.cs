namespace Build;

using DockerBuildXBuildSettings = Build.Cake.Docker.DockerBuildXBuildSettings;
using DockerBuildXImageToolsCreateSettings = Build.Cake.Docker.DockerBuildXImageToolsCreateSettings;

public static class DockerExtensions
{
    extension(ICakeContext context)
    {
        public void DockerBuildXBuild(DockerBuildXBuildSettings settings,
            DirectoryPath target)
        {
            ArgumentNullException.ThrowIfNull(context);
            var runner = context.CreateRunner<DockerBuildXBuildSettings>();
            runner.Run("buildx build", settings, [
                "--provenance=true",
                "--sbom=true",
                target.ToString().EscapeProcessArgument()
            ]);
        }

        public void DockerBuildXImageToolsCreate(DockerBuildXImageToolsCreateSettings settings, IEnumerable<string>? target = null)
        {
            ArgumentNullException.ThrowIfNull(context);
            var runner = context.CreateRunner<DockerBuildXImageToolsCreateSettings>();
            runner.Run("buildx imagetools create", settings, target?.ToArray() ?? []);
        }

        private GenericDockerRunner<TSettings> CreateRunner<TSettings>()
            where TSettings : AutoToolSettings, new() =>
            new(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
    }
}
