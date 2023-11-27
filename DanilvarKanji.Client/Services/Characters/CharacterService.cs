using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DanilvarKanji.Client.Services.Auth;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Shared.Requests.Characters;

namespace DanilvarKanji.Client.Services.Characters;

public class CharacterService : ICharacterService
{
    private readonly HttpClient _httpClient;

    public CharacterService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ServerApi");
    }

    public async Task<IEnumerable<CharacterDto?>?> ListCharactersAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Characters?PageNumber={pageNumber}&PageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return Enumerable.Empty<CharacterDto>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterDto>>() ??
                       Enumerable.Empty<CharacterDto>();
            }
            
            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
        }
        catch (HttpRequestException e)
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
            throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
        }
        catch (HttpRequestException e)
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
                await _httpClient.GetAsync($"api/Characters/{id}:KanjiMeanings?culture={culture}&takeQty={takeQty}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new List<string>();
                }

                return await response.Content.ReadFromJsonAsync<List<string>>();
            }

            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Dictionary<string, List<string>>> SetKanjiMeanings(IEnumerable<CharacterDto?>? CharacterItems,
        int takeQty, Culture culture)
    {
        Dictionary<string, List<string>> allKanjiMeanings = new Dictionary<string, List<string>>();

        if (CharacterItems != null)
            foreach (CharacterDto? characterItem in CharacterItems)
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
            throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }

    public async Task<IEnumerable<CharacterDto>> SearchCharacters(string searchTerm)
    {
        try
        {
            if (string.IsNullOrEmpty(searchTerm))
                searchTerm = "any";
                
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Characters/{searchTerm}:Search");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return new List<CharacterDto>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterDto>>() ??
                       Enumerable.Empty<CharacterDto>();
            }

            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }

    public async Task<CreateCharacterRequest?> AddCharacterAsync(CreateCharacterRequest request)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Characters", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CreateCharacterRequest>();
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