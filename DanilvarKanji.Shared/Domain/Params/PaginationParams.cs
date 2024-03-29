namespace DanilvarKanji.Shared.Domain.Params;

public class PaginationParams
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
}