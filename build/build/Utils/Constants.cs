namespace Build;

public static class Constants
{
    public const string ToolsDirectory = "./tools";
    public const string DockerHubRegistry = "docker.io";
    public const string GitHubContainerRegistry = "ghcr.io";
    public const string DockerImageName = "gittools/build-images";
    public const string DockerImageDeps = "gittools/deps";

    public static readonly Architecture[] ArchToBuild = [Architecture.Amd64, Architecture.Arm64];
    public static readonly string[] VersionsToBuild = ["8.0", "6.0"];
    public static readonly string[] VariantsToBuild = ["sdk", "runtime"];
    public static readonly string[] DockerDistrosToBuild =
    [
        "alpine.3.19",
        "alpine.3.20",
        "centos.stream.9",
        "debian.12",
        "fedora.40",
        "ubuntu.20.04",
        "ubuntu.22.04",
        "ubuntu.24.04"
    ];
    public static readonly string VersionForDockerLatest = VersionsToBuild[0];
}
