using System.Net.Http.Json;
using DanilvarKanji.Shared.DTO;
using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Client.Services.Characters;

public class CharacterService : ICharacterService
{
    private readonly HttpClient _httpClient;

    public CharacterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CharacterDto?>?> ListCharactersAsync()
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

    public async Task<CharacterDto?> GetCharacterAsync(string? id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Characters/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default;
                }

                return await response.Content.ReadFromJsonAsync<CharacterDto>();
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

    public async Task<List<string>?> GetCharacterKanjiMeanings(string? id, int takeQty, Culture culture)
    {
        try
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Characters/{id}:KanjiMeanings?takeQty={takeQty}&culture={culture}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new List<string>();
                }

                return await response.Content.ReadFromJsonAsync<List<string>>();
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

    public async Task<Dictionary<string, List<string>>> SetKanjiMeanings(IEnumerable<CharacterDto> CharacterItems, int takeQty, Culture culture)
    {
        Dictionary<string, List<string>> allKanjiMeanings = new Dictionary<string, List<string>>();

        foreach (CharacterDto characterItem in CharacterItems)
        {
            List<string>? kanjiMeanings = await GetCharacterKanjiMeanings(characterItem.Id, takeQty, culture);

            if (kanjiMeanings != null)
                allKanjiMeanings![characterItem.Id] = kanjiMeanings;
        }

        return allKanjiMeanings;
    }

    public string GetCharacterMnemonicByCulture(CharacterDto character, Culture culture = Culture.EnUS) =>
        character.Mnemonics
            .FirstOrDefault(x => x.Culture == culture)
            ?.Value;

    public async Task<IEnumerable<CharacterDto>> ListCharactersFilteredBy(string filter, string term)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Characters?$filter={filter.ToLower()} eq '{term}'");

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

    public async Task<HttpResponseMessage> AddCharacterAsync(CharacterDto character)
    {
        return await _httpClient.PostAsJsonAsync("api/characters", character);
    }
}