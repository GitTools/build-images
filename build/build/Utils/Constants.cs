public class Constants
{
    public const string ToolsDirectory = "./tools";
    public const string DockerHubRegistry = "docker.io";
    public const string GitHubContainerRegistry = "ghcr.io";
    public const string DockerImageName = "gittools/build-images";
    public const string DockerImageDeps = "gittools/deps";

    public static readonly string VersionForDockerLatest = "5.0";
    public static readonly string[] DistrosToSkip = { "alpine.3.12", "alpine.3.13", "alpine.3.14" };
    public static readonly string[] VersionsToBuild = { "6.0", "5.0", "3.1" };
    public static readonly string[] VariantsToBuild = { "sdk", "runtime" };
    public static readonly Architecture[] ArchToBuild = { Architecture.Amd64, Architecture.Arm64 };
    public static readonly string[] DockerDistrosToBuild =
    {
        "alpine.3.12",
        "alpine.3.13",
        "alpine.3.14",
        "centos.7",
        "centos.8",
        "debian.9",
        "debian.10",
        "debian.11",
        "fedora.33",
        "ubuntu.18.04",
        "ubuntu.20.04"
    };
}
