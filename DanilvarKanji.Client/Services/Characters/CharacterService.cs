using System.Net.Http.Json;
using DanilvarKanji.Shared.DTO;

namespace DanilvarKanji.Client.Services.Characters;

public class CharacterService : ICharacterService
{
    private readonly HttpClient _httpClient;

    public CharacterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CharacterDto?>> ListCharactersAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/Characters");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CharacterDto>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterDto>>() ??
                       Array.Empty<CharacterDto>();
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