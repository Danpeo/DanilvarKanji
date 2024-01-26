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

    public async Task<IEnumerable<GetAllFromCharacterResponse?>?> ListCharactersAsync(int pageNumber = 0,
        int pageSize = 0)
    {
        try
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Characters?PageNumber={pageNumber}&PageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return Enumerable.Empty<GetAllFromCharacterResponse>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<GetAllFromCharacterResponse>>() ??
                       Enumerable.Empty<GetAllFromCharacterResponse>();
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

    public async Task<IEnumerable<GetCharacterBaseInfoResponse?>?> ListLearnQueueAsync(int pageNumber = 0,
        int pageSize = 0, bool listOnlyDayDosage = false)
    {
        try
        {
            var uri = $"api/Characters/LearnQueue?PageNumber={pageNumber}&PageSize={pageSize}&listOnlyDayDosage={listOnlyDayDosage}";
            
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return Enumerable.Empty<GetCharacterBaseInfoResponse>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<GetCharacterBaseInfoResponse>>();
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

    public async Task<GetAllFromCharacterResponse?> GetCharacterAsync(string? id)
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

                return await response.Content.ReadFromJsonAsync<GetAllFromCharacterResponse>();
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

    public async Task<GetCharacterBaseInfoResponse?> GetNextInLearnQueue()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Characters/GetNextInLearnQueue");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                return await response.Content.ReadFromJsonAsync<GetCharacterBaseInfoResponse>();
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
        IEnumerable<GetAllFromCharacterResponse?>? CharacterItems,
        int takeQty, Culture culture)
    {
        Dictionary<string, List<string>> allKanjiMeanings = new Dictionary<string, List<string>>();

        if (CharacterItems != null)
            foreach (GetAllFromCharacterResponse? characterItem in CharacterItems)
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

    public string GetCharacterMnemonicByCulture(GetAllFromCharacterResponse getAllFromCharacter,
        Culture culture = Culture.EnUS) =>
        getAllFromCharacter.Mnemonics
            .FirstOrDefault(x => x.Culture == culture)
            ?.Value;

    public async Task<IEnumerable<GetAllFromCharacterResponse>> ListCharactersFilteredBy(string filter, string term)
    {
        try
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Characters?$filter={filter.ToLower()} eq '{term}'");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<GetAllFromCharacterResponse>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<GetAllFromCharacterResponse>>() ??
                       Array.Empty<GetAllFromCharacterResponse>();
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

    public async Task<IEnumerable<GetAllFromCharacterResponse>> SearchCharacters(string searchTerm)
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
                    return new List<GetAllFromCharacterResponse>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<GetAllFromCharacterResponse>>() ??
                       Enumerable.Empty<GetAllFromCharacterResponse>();
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

    public async Task DeleteCharacterAsync(string id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Characters/{id}");

            if (response.IsSuccessStatusCode)
                return;

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