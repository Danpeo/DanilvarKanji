using System.Net;
using System.Net.Http.Json;
using DanilvarKanji.Shared.Domain.Enumerations;
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

    public async Task<IEnumerable<CharacterResponseResponseFull?>?> ListCharactersAsync(int pageNumber = 0,
        int pageSize = 0)
    {
        HttpResponseMessage response =
            await _httpClient.GetAsync($"api/Characters?PageNumber={pageNumber}&PageSize={pageSize}");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return Enumerable.Empty<CharacterResponseResponseFull>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterResponseResponseFull>>() ??
                   Enumerable.Empty<CharacterResponseResponseFull>();
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
    }

    public async Task<IEnumerable<CharacterResponseBase?>?> ListLearnQueueAsync(int pageNumber = 0,
        int pageSize = 0, bool listOnlyDayDosage = false)
    {
        try
        {
            var uri =
                $"api/Characters/LearnQueue?PageNumber={pageNumber}&PageSize={pageSize}&listOnlyDayDosage={listOnlyDayDosage}";

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return Enumerable.Empty<CharacterResponseBase>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterResponseBase>>();
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

    public async Task<CharacterResponseResponseFull?> GetCharacterAsync(string? id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/Characters/{id}");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return default;

            return await response.Content.ReadFromJsonAsync<CharacterResponseResponseFull>();
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
    }

    public async Task<CharacterResponseBase?> GetNextInLearnQueue()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Characters/GetNextInLearnQueue");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                return await response.Content.ReadFromJsonAsync<CharacterResponseBase>();
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return default;

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

    public async Task<Dictionary<string, List<string>>> SetKanjiMeanings(
        IEnumerable<CharacterResponseResponseFull?>? CharacterItems,
        int takeQty, Culture culture)
    {
        Dictionary<string, List<string>> allKanjiMeanings = new Dictionary<string, List<string>>();

        if (CharacterItems != null)
            foreach (CharacterResponseResponseFull? characterItem in CharacterItems)
            {
                try
                {
                    List<string>? kanjiMeanings = await GetCharacterKanjiMeanings(characterItem.Id, takeQty, culture);

                    if (kanjiMeanings != null)
                        allKanjiMeanings![characterItem.Id] = kanjiMeanings;
                }
                catch (HttpRequestException e)
                {
                    return new Dictionary<string, List<string>>();
                }
            }

        return allKanjiMeanings;
    }

    public string GetCharacterMnemonicByCulture(CharacterResponseResponseFull character,
        Culture culture = Culture.EnUS) =>
        character.Mnemonics
            .FirstOrDefault(x => x.Culture == culture)
            ?.Value;

    public async Task<IEnumerable<CharacterResponseResponseFull>> ListCharactersFilteredBy(string filter, string term)
    {
        HttpResponseMessage response =
            await _httpClient.GetAsync($"api/Characters?$filter={filter.ToLower()} eq '{term}'");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return Enumerable.Empty<CharacterResponseResponseFull>();
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterResponseResponseFull>>() ??
                   Array.Empty<CharacterResponseResponseFull>();
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
    }

    public async Task<IEnumerable<CharacterResponseResponseFull>> SearchCharacters(string searchTerm)
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
                    return new List<CharacterResponseResponseFull>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterResponseResponseFull>>() ??
                       Enumerable.Empty<CharacterResponseResponseFull>();
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
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Characters", request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<CreateCharacterRequest>();
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
    }

    public async Task DeleteCharacterAsync(string id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Characters/{id}");

        if (response.IsSuccessStatusCode)
            return;

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
    }
}