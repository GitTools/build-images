using Cake.Json;

namespace Build;

public class SetMatrix : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (context.BuildSystem().IsRunningOnGitHubActions)
        {
            context.GitHubActions().Commands.SetOutputParameter("dockerDistros", context.SerializeJson(Constants.DockerDistros));
            context.GitHubActions().Commands.SetOutputParameter("dotnetVersions", context.SerializeJson(Constants.DotnetVersions));
            context.GitHubActions().Commands.SetOutputParameter("dotnetVariants", context.SerializeJson(Constants.DotnetVariants));
        }
        else
        {
            context.Information("Docker Distros: {0}", context.SerializeJson(Constants.DockerDistros));
            context.Information("Dotnet Versions: {0}", context.SerializeJson(Constants.DotnetVersions));
            context.Information("Dotnet Variants: {0}", context.SerializeJson(Constants.DotnetVariants));
        }
    }
}
