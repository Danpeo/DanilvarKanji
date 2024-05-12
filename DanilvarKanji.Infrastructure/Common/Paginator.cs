using DanilvarKanji.Domain.Shared.Params;

namespace DanilvarKanji.Infrastructure.Common;

public static class Paginator
{
  public static IEnumerable<T> Paginate<T>(IEnumerable<T> items, PaginationParams? paginationParams)
  {
    if (paginationParams.PageNumber != 0 && paginationParams.PageSize != 0)
      return items
        .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
        .Take(paginationParams.PageSize);

    return items;
  }
}