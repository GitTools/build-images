public class DockerImages
{
    public ICollection<DockerImage> All { get; private set; }

    public static DockerImages GetDockerImages(ICakeContext context, FilePath[] dockerfiles, string[] versions, string[] variants, string[] distros)
    {
        var toDockerImage = DockerImage();
        var dockerImages =
            (from version in versions
            from variant in variants
            from distro in distros
            select toDockerImage(distro, version, variant)).ToArray();

        return new DockerImages {
            All = dockerImages,
        };
    }

    private static Func<string, string, string, DockerImage> DockerImage()
    {
        return (distro, version, variant) => {
            return new DockerImage(distro: distro, version: version, variant: variant);
        };
    }
}

public class DockerImage
{
    public string Distro { get; private set; }
    public string Version { get; private set; }
    public string Variant { get; private set; }

    public DockerImage(string distro, string version, string variant)
    {
        Distro = distro;
        Version = version;
        Variant = variant;
    }

    public void Deconstruct(out string distro, out string version, out string variant)
    {
        distro = Distro;
        version = Version;
        variant = Variant;
    }
}

FilePath FindToolInPath(string tool)
{
    var pathEnv = EnvironmentVariable("PATH");
    if (string.IsNullOrEmpty(pathEnv) || string.IsNullOrEmpty(tool)) return tool;

    var paths = pathEnv.Split(new []{ IsRunningOnUnix() ? ':' : ';'},  StringSplitOptions.RemoveEmptyEntries);
    return paths.Select(path => new DirectoryPath(path).CombineWithFilePath(tool)).FirstOrDefault(filePath => FileExists(filePath.FullPath));
}

void DockerStdinLogin(string username, string password)
{
    var toolPath = FindToolInPath(IsRunningOnUnix() ? "docker" : "docker.exe");
    var args = new ProcessArgumentBuilder()
        .Append("login")
        .Append("--username").AppendQuoted(username)
        .Append("--password-stdin");

    var processStartInfo = new ProcessStartInfo(toolPath.ToString(), args.Render())
    {
        RedirectStandardInput = true,
        RedirectStandardError = true,
        UseShellExecute = false
    };

    using (var process = new Process { StartInfo = processStartInfo })
    {
        process.Start();
        process.StandardInput.WriteLine(password);
        process.StandardInput.Close();
        process.WaitForExit();
        if (process.ExitCode != 0) throw new Exception(toolPath.GetFilename() + " returned exit code " + process.ExitCode + ".");
    }
}

void DockerBuild(DockerImage dockerImage)
{
    var (distro, version, variant) = dockerImage;
    var workDir = DirectoryPath.FromString($"./src/linux/{distro}");

    var tags = GetDockerTags(dockerImage);
    var content = FileReadText($"{workDir}/Dockerfile");
    if (variant == "sdk") {
        content += "\nRUN dotnet tool install powershell --global";
        if (version == "3.1") {
            content += " --version 7.0.3";
        }
        else {
            content += "\nRUN ln -sf /root/.dotnet/tools/pwsh /usr/bin/pwsh";
        }
    }

    FileWriteText($"{workDir}/Dockerfile.build", content);

    var buildSettings = new DockerImageBuildSettings
    {
        Rm = true,
        Tag = tags,
        File = $"{workDir}/Dockerfile.build",
        BuildArg = new []{ $"DOTNET_VERSION={version}", $"DOTNET_VARIANT={variant}" },
        Pull = true,
    };

    DockerBuild(buildSettings, workDir.ToString());
}

void DockerPush(DockerImage dockerImage)
{
    var tags = GetDockerTags(dockerImage);

    foreach (var tag in tags)
    {
        DockerPush(tag);
    }
}

string[] GetDockerTags(DockerImage dockerImage) {
    var name = $"gittools/build-images";
    var (distro, version, variant) = dockerImage;

    var tags = new List<string> {
        $"{name}:{distro}-{variant}-{version}",
    };

    if (version == "5.0") {
        tags.AddRange(new[] {
            $"{name}:{distro}-{variant}-latest",
        });
    }

    return tags.ToArray();
}
