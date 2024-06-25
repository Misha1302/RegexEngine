namespace RegexEngine.FastLinqs;

public static class FastLinqs
{
    public static bool FastAll<T>(this IReadOnlyList<T> list, Func<T, bool> predicate, int startIndex = 0)
    {
        for (var index = startIndex; index < list.Count; index++)
            if (!predicate(list[index]))
                return false;

        return true;
    }

    public static bool FastStartsWith(this string a, string b, int offset)
    {
        if (a.Length - offset < b.Length)
            return false;

        for (var i = 0; i < b.Length; i++)
            if (a[i + offset] != b[i])
                return false;

        return true;
    }

    public static T? FastFirstOrDefault<T>(this IReadOnlyList<T> list, Func<T, bool> predicate)
    {
        for (var index = 0; index < list.Count; index++)
            if (predicate(list[index]))
                return list[index];

        return default;
    }
}