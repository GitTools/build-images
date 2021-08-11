using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Core.IO;
using Cake.Frosting;

public static class Extensions
{
    public static IEnumerable<Type> FindAllDerivedTypes(this Assembly assembly, Type baseType) =>
        from type in assembly.GetExportedTypes()
        let info = type.GetTypeInfo()
        where baseType.IsAssignableFrom(type) && info.IsClass && !info.IsAbstract
        select type;

    public static string GetTaskDescription(this Type task)
    {
        if (task is null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        var attribute = task.GetCustomAttribute<TaskDescriptionAttribute>();
        return attribute != null ? attribute.Description : string.Empty;
    }

    public static string GetTaskName(this Type task)
    {
        if (task is null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        var attribute = task.GetCustomAttribute<TaskNameAttribute>();
        return attribute != null ? attribute.Name : task.Name;
    }

    public static IEnumerable<string> GetDockerTagsForRepository(this DockerImage dockerImage, string repositoryName) {
        var (distro, version, variant) = dockerImage;

        var tags = new List<string> {
            $"{repositoryName}:{distro}-{variant}-{version}",
        };

        if (version == BuildLifetime.VersionsToBuild[0]) {
            tags.AddRange(new[] {
                $"{repositoryName}:{distro}-{variant}-latest",
            });
        }

        return tags.ToArray();
    }
    public static DirectoryPath Combine(this string path, string segment) => DirectoryPath.FromString(path).Combine(segment);
    public static FilePath CombineWithFilePath(this string path, string segment) => DirectoryPath.FromString(path).CombineWithFilePath(segment);
}
