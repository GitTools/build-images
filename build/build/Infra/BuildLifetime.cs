namespace Build;

public sealed class BuildLifetime : FrostingLifetime<BuildContext>
{
    public override void Setup(BuildContext context, ISetupContext info)
    {
        var architecture = context.HasArgument("arch") ? context.Argument<Architecture>("arch") : (Architecture?)null;

        var dockerRegistry = context.Argument("docker_registry", "").ToLower();
        var dotnetVersion = context.Argument("dotnet_version", "").ToLower();
        var dotnetVariant = context.Argument("dotnet_variant", "").ToLower();
        var dockerDistro = context.Argument("docker_distro", "").ToLower();
        var pushImages = context.Argument("push_images", false);

        context.Information($"Building for Version: {dotnetVersion}, Variant: {dotnetVariant}, Distro: {dockerDistro}, Push: {pushImages}");

        var archs = architecture.HasValue ? [architecture.Value] : Constants.ArchToBuild;
        var variants = string.IsNullOrWhiteSpace(dotnetVariant) ? Constants.DotnetVariants : [dotnetVariant];

        var versions = dotnetVersion switch
        {
            _ when dotnetVersion.IsNullOrWhiteSpace() => Constants.DotnetVersions,
            _ when dotnetVersion.IsEqualInvariant("lts-latest") => [Constants.DotnetLtsLatest],
            _ => [dotnetVersion]
        };

        var distros = dockerDistro switch
        {
            _ when dockerDistro.IsNullOrWhiteSpace() => Constants.DockerDistros,
            _ when dockerDistro.IsEqualInvariant("distro-latest") => [Constants.AlpineLatest],
            _ => [dockerDistro]
        };

        context.PushImages = pushImages;
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
