namespace Build;

public static class DockerExtensions
{
    public static void DockerBuild(
        this ICakeContext context,
        DockerImageBuildSettings settings,
        string path, params string[] args)
    {
        GenericDockerRunner<DockerImageBuildSettings> genericDockerRunner =
            new(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);

        string str1;
        switch (string.IsNullOrEmpty(path))
        {
            case false:
                {
                    var str2 = path.Trim();
                    str1 = str2.Length <= 1 || !str2.StartsWith("\"") || !str2.EndsWith("\"") ? "\"" + path + "\"" : path;
                    break;
                }
            default:
                str1 = path;
                break;
        }
        var additional = args.Concat(new[] {
            str1
        }).ToArray();
        genericDockerRunner.Run("buildx build", settings, additional);
    }

    public static void DockerManifestRemove(this ICakeContext context, string tag) => context.DockerCustomCommand($"manifest rm {tag}");
}
