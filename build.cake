// Install modules
#module nuget:?package=Cake.DotNetTool.Module&version=0.2.0

// Install addins.
#addin "nuget:?package=Cake.Docker&version=0.10.0"
#addin "nuget:?package=Cake.FileHelpers&version=3.2.0"
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
    var distro = Argument("dotnet_distro", "").ToLower();

    Information($"Version: {version}, Variant: {variant}");
    var versions = string.IsNullOrWhiteSpace(version) ? new[] { "3.1", "5.0" } : new[] { version };
    var variants = string.IsNullOrWhiteSpace(variant) ? new[] { "sdk", "runtime" } : new[] { variant };
    var distros  = string.IsNullOrWhiteSpace(distro) ? new[] { "alpine.3.12-x64", "centos.7-x64", "debian.9-x64", "debian.10-x64", "fedora.33-x64", "ubuntu.16.04-x64", "ubuntu.18.04-x64", "ubuntu.20.04-x64" } : new[] { distro };
    var docker = DockerImages.GetDockerImages(context, dockerFiles, versions, variants, distros);

    images = docker.All;
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
