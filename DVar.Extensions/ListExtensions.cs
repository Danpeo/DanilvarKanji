namespace DVar.Extensions;

public static class ListExtensions
{
    public static void RemoveLast<T>(this List<T> list)
    {
        if (list.Any()) list.RemoveAt(list.Count - 1);
    }

    public static void ModifyElement<T>(this List<T> list, Predicate<T> match, T modifiedElement)
    {
        int index = list.FindIndex(match);

        if (index != -1)
        {
            list[index] = modifiedElement;
        }
    }

    public static void Shuffle<T>(this List<T> list)
    {
        var rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}