namespace Build;

public static class DockerImageExtensions
{
    public static IEnumerable<string> GetDockerTags(this DockerDepsImage dockerImage, string dockerRegistry,
        Architecture? arch = null)
    {
        var tags = new List<string>();

        if (dockerImage is DockerImage image)
        {
            var (distro, version, variant, _) = image;

            tags.Add($"{dockerRegistry}/{Constants.DockerImageName}:{distro}-{variant}-{version}");

            if (version == Constants.DockerDistroLatest)
            {
                tags.Add($"{dockerRegistry}/{Constants.DockerImageName}:{distro}-{variant}-latest");
            }
        }
        else
        {
            tags.Add($"{dockerRegistry}/{Constants.DockerImageDeps}:{dockerImage.Distro}");
        }

        if (!arch.HasValue) return tags;

        var suffix = arch.Value.ToSuffix();
        return tags.Select(x => $"{x}-{suffix}");
    }
}
