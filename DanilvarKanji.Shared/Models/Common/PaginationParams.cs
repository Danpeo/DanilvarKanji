namespace DanilvarKanji.Shared.Models.Common;

public class PaginationParams
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = int.MaxValue;
}