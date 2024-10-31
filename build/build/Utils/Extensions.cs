namespace Build;

public static class Extensions
{
    private static readonly char[] CharsThatRequireQuoting = [' ', '"'];
    private static readonly char[] CharsThatRequireEscaping = ['\\', '"'];

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

    /// <summary>
    /// Escapes arbitrary values so that the process receives the exact string you intend and injection is impossible.
    /// Spec: https://msdn.microsoft.com/en-us/library/bb776391.aspx
    /// </summary>
    public static string EscapeProcessArgument(this string literalValue, bool alwaysQuote = false)
    {
        if (string.IsNullOrEmpty(literalValue)) return "\"\"";

        if (literalValue.IndexOfAny(CharsThatRequireQuoting) == -1) // Happy path
        {
            if (!alwaysQuote) return literalValue;
            if (literalValue[^1] != '\\') return "\"" + literalValue + "\"";
        }

        var sb = new StringBuilder(literalValue.Length + 8).Append('"');

        var nextPosition = 0;
        while (true)
        {
            var nextEscapeChar = literalValue.IndexOfAny(CharsThatRequireEscaping, nextPosition);
            if (nextEscapeChar == -1) break;

            sb.Append(literalValue, nextPosition, nextEscapeChar - nextPosition);
            nextPosition = nextEscapeChar + 1;

            switch (literalValue[nextEscapeChar])
            {
                case '"':
                    sb.Append("\\\"");
                    break;
                case '\\':
                    var numBackslashes = 1;
                    while (nextPosition < literalValue.Length && literalValue[nextPosition] == '\\')
                    {
                        numBackslashes++;
                        nextPosition++;
                    }
                    if (nextPosition == literalValue.Length || literalValue[nextPosition] == '"')
                        numBackslashes <<= 1;

                    for (; numBackslashes != 0; numBackslashes--)
                        sb.Append('\\');
                    break;
            }
        }

        sb.Append(literalValue, nextPosition, literalValue.Length - nextPosition).Append('"');
        return sb.ToString();
    }
}
