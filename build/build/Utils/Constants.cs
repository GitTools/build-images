namespace Build;

public static class Constants
{
    public const string ToolsDirectory = "./tools";
    public const string DockerHubRegistry = "docker.io";
    public const string GitHubContainerRegistry = "ghcr.io";
    public const string DockerImageName = "gittools/build-images";
    public const string DockerImageDeps = "gittools/deps";

    public static readonly Architecture[] ArchToBuild = [Architecture.Amd64, Architecture.Arm64];
    public static readonly string[] DotnetVariants = ["sdk", "runtime"];

    public const string DotnetLtsLatest = "8.0";
    public static readonly string[] DotnetVersions = [DotnetLtsLatest, "9.0"];

    public const string AlpineLatest = "alpine.3.22";
    public const string CentosLatest = "centos.stream.9";
    public const string DebianLatest = "debian.12";
    public const string FedoraLatest = "fedora.42";
    public const string UbuntuLatest = "ubuntu.24.04";

    public const string DockerDistroLatest = DebianLatest;

    public static readonly string[] DockerDistros =
    [
        AlpineLatest,
        CentosLatest,
        DebianLatest,
        FedoraLatest,
        UbuntuLatest,
        "ubuntu.22.04"
    ];
}
