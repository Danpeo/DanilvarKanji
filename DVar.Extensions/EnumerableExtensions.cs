namespace DVar.Extensions;

public static class EnumerableExtensions
{
    public static void RemoveLast<T>(this List<T> list)
    {
        if (list.Any())
            list.RemoveAt(list.Count - 1);
    }

    public static void ModifyElement<T>(this List<T> list, Predicate<T> match, T modifiedElement)
    {
        int index = list.FindIndex(match);

        if (index != -1) list[index] = modifiedElement;
    }

    public static void Shuffle<T>(this List<T> list)
    {
        var random = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    public static void Shuffle<TArray>(this TArray[] array)
    {
        var random = new Random();
        var n = array.Length;
        while (n > 1)
        {
            n--;
            var k = random.Next(n + 1);
            (array[k], array[n]) = (array[n], array[k]);
        }
    }

    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> enumerable, int n)
    {
        IEnumerable<T> e = enumerable.ToList();
        return e.Skip(Math.Max(0, e.Count() - n));
    }

    public static IEnumerable<T> Rotate<T>(this IEnumerable<T> enumerable, int n)
    {
        IEnumerable<T> e = enumerable.ToList();
        n %= e.Count();
        var rotatedArray = e.Reverse()
            .Take(n)
            .Concat(e.Reverse().Skip(n))
            .ToArray();
        return rotatedArray;
    }

    public static void Rotate<T>(this T[] arr, int n)
    {
        n %= arr.Length;
        Array.Reverse(arr);
        Array.Reverse(arr, 0, n);
        Array.Reverse(arr, n, arr.Length - n);
    }
}