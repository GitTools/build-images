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

    public static bool SkipArm64Image(this ICakeContext context, DockerImage dockerImage)
    {
        var (distro, arch) = dockerImage;
        if (arch != Architecture.Arm64 ) return false;
        if (!Constants.DistrosToSkip.Contains(distro)) return false;
        if (dockerImage.Version != "3.1") return false;

        context.Information($"Skipping Distro: {distro}, Arch: {arch}");
        return true;
    }

}
