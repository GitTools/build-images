public class Constants
{
    public const string ToolsDirectory = "./tools";
    public static readonly string DockerHubRegistry = $"docker.io/{DockerImageName}";
    public static readonly string GitHubContainerRegistry = $"ghcr.io/{DockerImageName}";
    private const string DockerImageName = "gittools/build-images";
}
