namespace Danilvar.Components.Pagination;

public class PaginatedList<T> : List<T>
{
    public PaginatedList()
    {
    }

    public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public int PageIndex { get; }
    public int TotalPages { get; }

    public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
    {
        IEnumerable<T> enumerable = source.ToList();
        var items = enumerable.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        var count = enumerable.Count();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}