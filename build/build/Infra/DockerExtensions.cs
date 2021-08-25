using Cake.Core;
using Cake.Docker;

public static class DockerExtensions
{
    public static void DockerBuild(
        this ICakeContext context,
        DockerImageBuildSettings settings,
        string path)
    {
        GenericDockerRunner<DockerImageBuildSettings> genericDockerRunner =
            new(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);

        string str1;
        switch (string.IsNullOrEmpty(path))
        {
            case false:
                {
                    string str2 = path.Trim();
                    str1 = str2.Length <= 1 || !str2.StartsWith("\"") || !str2.EndsWith("\"") ? "\"" + path + "\"" : path;
                    break;
                }
            default:
                str1 = path;
                break;
        }
        string[] additional = new string[2] { str1, "-o type=image" };
        genericDockerRunner.Run("buildx build", settings, additional);
    }
}
