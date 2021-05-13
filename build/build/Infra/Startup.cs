using Cake.Frosting;
using Microsoft.Extensions.DependencyInjection;

public class Startup : IFrostingStartup
{
    public void Configure(IServiceCollection services)
    {
        services.UseLifetime<BuildLifetime>();
        services.UseTaskLifetime<BuildTaskLifetime>();
    }
}
