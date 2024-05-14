using System.Net;
using System.Net.Http.Json;
using DanilvarKanji.Client.Models.Responses.JMdict;

namespace DanilvarKanji.Client.Services.Dictionary;

public class DictionaryService : IDictionaryService
{
    private readonly HttpClient _httpClient;

    public DictionaryService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("JMdict");
    }

    public async Task<IEnumerable<Word?>?> ListWordsByKanaAsync(string entry, bool useRomaji = false)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"Words/kana/{entry}?useRomaji={useRomaji}"
            );

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return Enumerable.Empty<Word>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Word>>()
                       ?? Enumerable.Empty<Word>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Word?>?> ListWordsByKanjiAsync(string entry)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Words/kanji/{entry}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return Enumerable.Empty<Word>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Word>>()
                       ?? Enumerable.Empty<Word>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Word?>?> ListWordsByTranslationAsync(string entry)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Words/translation/{entry}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return Enumerable.Empty<Word>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Word>>()
                       ?? Enumerable.Empty<Word>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}