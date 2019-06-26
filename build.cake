// Install modules
#module nuget:?package=Cake.DotNetTool.Module&version=0.2.0

// Install addins.
#addin "nuget:?package=Cake.Docker&version=0.10.0"
#addin "nuget:?package=Cake.Incubator&version=5.0.1"

// Load other scripts.
#load "./utils.cake"

using System.Diagnostics;
///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

ICollection<DockerImage> images;
Setup(context =>
{
    var dockerFiles = GetFiles("./src/**/Dockerfile").ToArray();
    var version = Argument("dotnet_version", "").ToLower();
    var variant = Argument("dotnet_variant", "").ToLower();

    Information($"Version: {version}, Variant: {variant}");
    var versions = string.IsNullOrWhiteSpace(version) ? new[] { "2.1", "2.2" } : new[] { version };
    var variants = string.IsNullOrWhiteSpace(variant) ? new[] { "sdk", "runtime" } : new[] { variant };
    var docker = DockerImages.GetDockerImages(context, dockerFiles, versions, variants);

    images = IsRunningOnWindows()
            ? docker.Windows
            : docker.Linux;
});

Task("Docker-Build")
    .Does(() =>
{
    foreach(var dockerImage in images)
    {
        DockerBuild(dockerImage);
    }
});

Task("Publish-DockerHub")
    .IsDependentOn("Docker-Build")
    .Does(() =>
{
    var username = EnvironmentVariable("DOCKER_USERNAME");
    if (string.IsNullOrEmpty(username)) {
        throw new InvalidOperationException("Could not resolve Docker user name.");
    }

    var password = EnvironmentVariable("DOCKER_PASSWORD");
    if (string.IsNullOrEmpty(password)) {
        throw new InvalidOperationException("Could not resolve Docker password.");
    }

    DockerStdinLogin(username, password);

    foreach(var dockerImage in images)
    {
        DockerPush(dockerImage);
    }

    DockerLogout();
})
.OnError(exception =>
{
    Information("Publish-DockerHub Task failed, but continuing with next Task...");
    Error(exception.Dump());
});

Task("Default")
    .IsDependentOn("Publish-DockerHub");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
RunTarget(target);
