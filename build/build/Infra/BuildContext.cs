using System.Collections.Generic;
using Cake.Core;
using Cake.Frosting;

public record DockerImage(string Distro, string Version, string Variant);

public class BuildContext : FrostingContext
{
    public IEnumerable<DockerImage> Images { get; set; } = new List<DockerImage>();
    public string DockerRepository { get; set; } = Constants.DockerHubRegistry;
    public BuildContext(ICakeContext context)
        : base(context)
    {
    }
}
