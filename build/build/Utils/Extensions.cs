using System.Buffers;

namespace Build;

public static class Extensions
{
    private static readonly SearchValues<char> CharsRequiringQuoting = SearchValues.Create(' ', '"');
    private static readonly SearchValues<char> CharsRequiringEscaping = SearchValues.Create('\\', '"');

    public static IEnumerable<Type> FindAllDerivedTypes(this Assembly assembly, Type baseType) =>
        assembly.GetExportedTypes()
            .Where(t => baseType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

    extension(Type task)
    {
        public string GetTaskDescription()
        {
            ArgumentNullException.ThrowIfNull(task);
            return task.GetCustomAttribute<TaskDescriptionAttribute>()?.Description ?? string.Empty;
        }

        public string GetTaskName()
        {
            ArgumentNullException.ThrowIfNull(task);
            var attribute = task.GetCustomAttribute<TaskNameAttribute>();
            return attribute?.Name ?? task.Name;
        }
    }

    public static string ToSuffix(this Architecture arch) =>
        arch.ToString().ToLowerInvariant();

    extension(string value)
    {
        public bool IsNullOrWhiteSpace() =>
            string.IsNullOrWhiteSpace(value);

        public bool IsEqualInvariant(string other) =>
            string.Equals(value, other, StringComparison.InvariantCulture);
    }

    /// <summary>
    /// Escapes arbitrary values so that the process receives the exact string you intend and injection is impossible.
    /// Spec: https://msdn.microsoft.com/en-us/library/bb776391.aspx
    /// </summary>
    public static string EscapeProcessArgument(this string literalValue, bool alwaysQuote = false)
    {
        if (string.IsNullOrEmpty(literalValue)) return "\"\"";

        if (literalValue.AsSpan().IndexOfAny(CharsRequiringQuoting) == -1) // Happy path
        {
            if (!alwaysQuote) return literalValue;
            if (literalValue[^1] != '\\') return $"\"{literalValue}\"";
        }

        return BuildEscapedArgument(literalValue);
    }

    private static string BuildEscapedArgument(string s)
    {
        var sb = new StringBuilder(s.Length + 8).Append('"');
        var nextPosition = 0;

        while (true)
        {
            var relativeIndex = s.AsSpan(nextPosition).IndexOfAny(CharsRequiringEscaping);
            if (relativeIndex == -1) break;

            var nextEscapeChar = nextPosition + relativeIndex;
            sb.Append(s, nextPosition, relativeIndex);
            nextPosition = nextEscapeChar + 1;

            if (s[nextEscapeChar] == '"')
                sb.Append("\\\"");
            else
                nextPosition = AppendEscapedBackslashes(sb, s, nextPosition);
        }

        return sb.Append(s, nextPosition, s.Length - nextPosition).Append('"').ToString();
    }

    private static int AppendEscapedBackslashes(StringBuilder sb, string s, int nextPosition)
    {
        var numBackslashes = 1;
        while (nextPosition < s.Length && s[nextPosition] == '\\')
        {
            numBackslashes++;
            nextPosition++;
        }
        if (nextPosition == s.Length || s[nextPosition] == '"')
            numBackslashes <<= 1;

        sb.Append('\\', numBackslashes);
        return nextPosition;
    }
}
