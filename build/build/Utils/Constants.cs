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

    public const string DotnetLtsLatest = "10.0";
    public static readonly string[] DotnetVersions = [DotnetLtsLatest, "11.0"];
    public const string Dotnet11SdkVersion = "11.0.100-rc.1.26425.128";
    public const string Dotnet11RuntimeVersion = "11.0.0-rc.1.26425.128";

    public const string AlpineLatest = "alpine.3.23";
    public const string CentosLatest = "centos.stream.10";
    public const string DebianLatest = "debian.13";
    public const string FedoraLatest = "fedora.44";
    public const string UbuntuLatest = "ubuntu.26.04";

    public const string DockerDistroLatest = UbuntuLatest;

    public static readonly string[] DockerDistros =
    [
        AlpineLatest,
        CentosLatest,
        DebianLatest,
        FedoraLatest,
        UbuntuLatest,
        "ubuntu.24.04"
    ];
}
