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

    public const string VersionCurrent = "6.0";
    public const string VersionLatest = "8.0";
    public static readonly string[] DotnetVersions = [VersionCurrent, VersionLatest];

    public const string AlpineLatest = "alpine.3.20";
    public const string CentosStreamLatest = "centos.stream.9";
    public const string DebianLatest = "debian.12";
    public const string FedoraLatest = "fedora.40";
    public const string Ubuntu2004 = "ubuntu.20.04";
    public const string Ubuntu2204 = "ubuntu.22.04";
    public const string Ubuntu2404 = "ubuntu.24.04";

    public const string DockerDistroLatest = DebianLatest;

    public static readonly string[] DockerDistros =
    [
        AlpineLatest,
        CentosStreamLatest,
        DebianLatest,
        FedoraLatest,
        Ubuntu2004,
        Ubuntu2204,
        Ubuntu2404
    ];
}
