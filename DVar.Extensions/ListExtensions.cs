namespace DVar.Extensions;

public static class ListExtensions
{
    public static void RemoveLast<T>(this List<T> list)
    {
        if (list.Any()) list.RemoveAt(list.Count - 1);
    }

    public static void ModifyElement<T>(this List<T> list,  Predicate<T> match, T modifiedElement)
    {
        int index = list.FindIndex(match);

        if (index != -1)
        {
            list[index] = modifiedElement;
        }
    }
}