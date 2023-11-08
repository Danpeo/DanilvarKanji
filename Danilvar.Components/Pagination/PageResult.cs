namespace Danilvar.Components.Pagination;

public class PageResult<T> : PaginationInfo where T : class
{
    public T[] Results { get; set; }
}