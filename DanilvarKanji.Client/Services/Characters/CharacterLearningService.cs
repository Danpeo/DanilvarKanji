using System.Net.Http.Json;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Shared.Requests.CharacterLearnings;

namespace DanilvarKanji.Client.Services.Characters;

public class CharacterLearningService : ICharacterLearningService
{
    private readonly HttpClient _httpClient;

    public CharacterLearningService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ServerApi");
    }
    
    public async Task<CharacterLearning?> CreateCharacterLearningAsync(CreateCharacterLearningRequest request)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/CharacterLearnings", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CharacterLearning>();
            }

            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}