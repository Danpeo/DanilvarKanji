namespace DanilvarKanji.Domain.Params;

public class PaginationParams
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = int.MaxValue;
}