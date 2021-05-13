using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Core;

public static class ContextExtensions
{
    public static void StartGroup(this ICakeContext context, string title)
    {
        var buildSystem = context.BuildSystem();
        var startGroup = "[group]";
        if (buildSystem.IsRunningOnAzurePipelines || buildSystem.IsRunningOnAzurePipelinesHosted)
        {
            startGroup = "##[group]";
        }
        else if (buildSystem.IsRunningOnGitHubActions)
        {
            startGroup = "::group::";
        }
        context.Information($"{startGroup}{title}");
    }
    public static void EndGroup(this ICakeContext context)
    {
        var buildSystem = context.BuildSystem();
        var endgroup = "[endgroup]";
        if (buildSystem.IsRunningOnAzurePipelines || buildSystem.IsRunningOnAzurePipelinesHosted)
        {
            endgroup = "##[endgroup]";
        }
        else if (buildSystem.IsRunningOnGitHubActions)
        {
            endgroup = "::endgroup::";
        }
        context.Information($"{endgroup}");
    }
}
