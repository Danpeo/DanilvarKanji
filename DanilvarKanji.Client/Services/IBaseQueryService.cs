namespace DanilvarKanji.Client.Services;

public interface IBaseQueryService<TItem>
{
  Task<IEnumerable<TItem>> ListItemsFilteredBy(string items, string filter, string term);
}