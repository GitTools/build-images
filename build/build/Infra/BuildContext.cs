namespace Build;

public enum Architecture
{
    Arm64,
    Amd64
}

public record DockerDepsImage(string Distro, Architecture Architecture);
public record DockerImage(string Distro, string Version, string Variant, Architecture Architecture) : DockerDepsImage(Distro, Architecture);

public class BuildContext(ICakeContext context) : FrostingContext(context)
{
    public bool PushImages { get; set; }
    public IEnumerable<DockerDepsImage> DepsImages { get; set; } = new List<DockerDepsImage>();
    public IEnumerable<DockerImage> Images { get; set; } = new List<DockerImage>();
    public string DockerRegistry { get; set; } = Constants.DockerHubRegistry;
}