namespace DanilvarKanji.Domain.Shared.Params;

/*public class PaginationParams
{
    public int PageNumber { get; } = 1;
    public int PageSize { get; } = int.MaxValue;

    public PaginationParams()
    {
    }

    public PaginationParams(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}*/

public record PaginationParams(int PageNumber, int PageSize);