using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DanilvarKanji.Client.Services.Auth;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Shared.Requests.Characters;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Client.Services.Characters;

public class CharacterService : ICharacterService
{
    private readonly HttpClient _httpClient;

    public CharacterService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ServerApi");
    }

    public async Task<IEnumerable<CharacterResponse?>?> ListCharactersAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Characters?PageNumber={pageNumber}&PageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return Enumerable.Empty<CharacterResponse>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterResponse>>() ??
                       Enumerable.Empty<CharacterResponse>();
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

    public async Task<CharacterResponse?> GetCharacterAsync(string? id)
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

                return await response.Content.ReadFromJsonAsync<CharacterResponse>();
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

    public async Task<Dictionary<string, List<string>>> SetKanjiMeanings(IEnumerable<CharacterResponse?>? CharacterItems,
        int takeQty, Culture culture)
    {
        Dictionary<string, List<string>> allKanjiMeanings = new Dictionary<string, List<string>>();

        if (CharacterItems != null)
            foreach (CharacterResponse? characterItem in CharacterItems)
            {
                List<string>? kanjiMeanings = await GetCharacterKanjiMeanings(characterItem.Id, takeQty, culture);

                if (kanjiMeanings != null)
                    allKanjiMeanings![characterItem.Id] = kanjiMeanings;
            }

        return allKanjiMeanings;
    }

    public string GetCharacterMnemonicByCulture(CharacterResponse character, Culture culture = Culture.EnUS) =>
        character.Mnemonics
            .FirstOrDefault(x => x.Culture == culture)
            ?.Value;

    public async Task<IEnumerable<CharacterResponse>> ListCharactersFilteredBy(string filter, string term)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Characters?$filter={filter.ToLower()} eq '{term}'");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CharacterResponse>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterResponse>>() ??
                       Array.Empty<CharacterResponse>();
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

    public async Task<IEnumerable<CharacterResponse>> SearchCharacters(string searchTerm)
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
                    return new List<CharacterResponse>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterResponse>>() ??
                       Enumerable.Empty<CharacterResponse>();
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