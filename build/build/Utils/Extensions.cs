public static class Extensions
{
    public static IEnumerable<Type> FindAllDerivedTypes(this Assembly assembly, Type baseType) =>
        from type in assembly.GetExportedTypes()
        let info = type.GetTypeInfo()
        where baseType.IsAssignableFrom(type) && info.IsClass && !info.IsAbstract
        select type;

    public static string GetTaskDescription(this Type task)
    {
        ArgumentNullException.ThrowIfNull(task);

        var attribute = task.GetCustomAttribute<TaskDescriptionAttribute>();
        return attribute != null ? attribute.Description : string.Empty;
    }

    public static string GetTaskName(this Type task)
    {
        ArgumentNullException.ThrowIfNull(task);

        var attribute = task.GetCustomAttribute<TaskNameAttribute>();
        return attribute != null ? attribute.Name : task.Name;
    }

    public static string ToSuffix(this Architecture arch) => arch.ToString().ToLower();

    public static DirectoryPath Combine(this string path, string segment) => DirectoryPath.FromString(path).Combine(segment);
    public static FilePath CombineWithFilePath(this string path, string segment) => DirectoryPath.FromString(path).CombineWithFilePath(segment);
}
