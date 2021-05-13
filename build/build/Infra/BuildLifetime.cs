using System.Linq;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;

public sealed class BuildLifetime : FrostingLifetime<BuildContext>
{
    public static readonly string[] VersionsToBuild = { "5.0", "3.1" };
    public static readonly string[] VariantsToBuild = { "sdk", "runtime" };

    public static readonly string[] DockerDistrosToBuild =
    {
        "alpine.3.12-x64",
        "centos.7-x64",
        "centos.8-x64",
        "debian.9-x64",
        "debian.10-x64",
        "fedora.33-x64",
        "ubuntu.16.04-x64",
        "ubuntu.18.04-x64",
        "ubuntu.20.04-x64"
    };


    public override void Setup(BuildContext context)
    {
        var dotnetVersion = context.Argument("dotnet_version", "").ToLower();
        var dotnetVariant = context.Argument("dotnet_variant", "").ToLower();
        var dockerDistro = context.Argument("dotnet_distro", "").ToLower();

        context.Information($"Building for Version: {dotnetVersion}, Variant: {dotnetVariant}, Distro: {dockerDistro}");

        var versions = string.IsNullOrWhiteSpace(dotnetVersion) ? VersionsToBuild : new[] { dotnetVersion };
        var variants = string.IsNullOrWhiteSpace(dotnetVariant) ? VariantsToBuild : new[] { dotnetVariant };
        var distros = string.IsNullOrWhiteSpace(dockerDistro) ? DockerDistrosToBuild : new[] { dockerDistro };

        context.Images = from version in versions
                         from variant in variants
                         from distro in distros
                         select new DockerImage(distro, version, variant);
    }

    public override void Teardown(BuildContext context, ITeardownContext info)
    {
        context.Information("Tearing things down...");
    }
}
