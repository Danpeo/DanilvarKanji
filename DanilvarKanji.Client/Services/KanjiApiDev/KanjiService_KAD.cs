using System.Net;
using System.Net.Http.Json;
using DanilvarKanji.Client.Models.Responses;

namespace DanilvarKanji.Client.Services.KanjiApiDev;

public class KanjiService_KAD : IKanjiService
{
    private readonly HttpClient _httpClient;

    public KanjiService_KAD(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("KanjiApiDev");
    }

    public async Task<GetKanjiResponse_KAD?> GetKanjiAsync(string kanji)
    {
        try
        {
            var encodedKanji = WebUtility.UrlEncode(kanji);
            HttpResponseMessage response = await _httpClient.GetAsync($"kanji/{encodedKanji}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                return await response.Content.ReadFromJsonAsync<GetKanjiResponse_KAD>();
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