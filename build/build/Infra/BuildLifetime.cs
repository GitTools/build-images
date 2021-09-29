using System.Linq;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;

public sealed class BuildLifetime : FrostingLifetime<BuildContext>
{
    public static readonly string[] VersionsToBuild = { "5.0", "3.1" };
    private static readonly string[] VariantsToBuild = { "sdk", "runtime" };
    private static readonly Architecture[] ArchToBuild = { Architecture.Amd64, Architecture.Arm64 };

    private static readonly string[] DockerDistrosToBuild =
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

    public override void Setup(BuildContext context)
    {
        var architecture = context.HasArgument("arch") ? context.Argument<Architecture>("arch") : (Architecture?)null;

        var dockerRegistry = context.Argument("docker_registry", "").ToLower();
        var dotnetVersion = context.Argument("dotnet_version", "").ToLower();
        var dotnetVariant = context.Argument("dotnet_variant", "").ToLower();
        var dockerDistro = context.Argument("dotnet_distro", "").ToLower();

        context.Information($"Building for Version: {dotnetVersion}, Variant: {dotnetVariant}, Distro: {dockerDistro}");

        var versions = string.IsNullOrWhiteSpace(dotnetVersion) ? VersionsToBuild : new[] { dotnetVersion };
        var variants = string.IsNullOrWhiteSpace(dotnetVariant) ? VariantsToBuild : new[] { dotnetVariant };
        var distros = string.IsNullOrWhiteSpace(dockerDistro) ? DockerDistrosToBuild : new[] { dockerDistro };
        var archs = architecture.HasValue ? new[] { architecture.Value } : ArchToBuild;

        context.DepsImages = from distro in distros
                             from arch in archs
                             select new DockerDepsImage(distro, arch);

        context.Images = from version in versions
                         from variant in variants
                         from deps in context.DepsImages
                         select new DockerImage(deps.Distro, version, variant, deps.Architecture);

        context.DockerRegistry = dockerRegistry.ToLower() switch
        {
            "dockerhub" => Constants.DockerHubRegistry,
            "github" => Constants.GitHubContainerRegistry,
            _ => Constants.DockerHubRegistry
        };
    }

    public override void Teardown(BuildContext context, ITeardownContext info)
    {
        context.Information("Tearing things down...");
    }
}
