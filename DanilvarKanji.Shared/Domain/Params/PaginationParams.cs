namespace DanilvarKanji.Shared.Domain.Params;

public class PaginationParams
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = int.MaxValue;

    public PaginationParams()
    {
        
    }

    public PaginationParams(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}