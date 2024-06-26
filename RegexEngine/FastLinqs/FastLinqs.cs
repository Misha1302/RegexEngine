namespace RegexEngine.FastLinqs;

public static class FastLinqs
{
    public static bool FastAll<T>(this List<T> list, Func<T, bool> predicate, int startIndex = 0)
    {
        for (var index = startIndex; index < list.Count; index++)
            if (!predicate(list[index]))
                return false;

        return true;
    }

    public static bool FastStartsWith(this string a, string b, int offset)
    {
        var s1 = a.AsSpan(offset);
        var s2 = b.AsSpan();

        return s1.CommonPrefixLength(s2) == s2.Length;
    }

    public static T? FastFirstOrDefault<T>(this List<T> list, Func<T, bool> predicate)
    {
        for (var index = 0; index < list.Count; index++)
            if (predicate(list[index]))
                return list[index];

        return default;
    }
}