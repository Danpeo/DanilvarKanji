using System.Net.Http.Json;

namespace DanilvarKanji.Client.Services;

public class BaseQueryService<TItem> : IBaseQueryService<TItem>
{
    private readonly HttpClient _httpClient;

    public BaseQueryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<TItem>> ListItemsFilteredBy(string items, string filter, string term)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/{items}?$filter={filter.ToLower()} eq '{term}'");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<TItem>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<TItem>>() ??
                       Array.Empty<TItem>();
            }

            string message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode} message: {message}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }
}