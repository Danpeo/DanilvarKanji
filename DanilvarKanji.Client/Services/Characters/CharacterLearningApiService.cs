using System.Net;
using System.Net.Http.Json;
using DanilvarKanji.Client.State;
using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Requests.CharacterLearnings;
using DanilvarKanji.Shared.Responses.CharacterLearning;

namespace DanilvarKanji.Client.Services.Characters;

public class CharacterLearningApiService : ICharacterLearningApiService
{
    private readonly HttpClient _httpClient;
    private readonly AppState _appState;

    public CharacterLearningApiService(IHttpClientFactory factory, AppState appState)
    {
        _appState = appState;
        _httpClient = factory.CreateClient("ServerApi");
    }

    public async Task<CharacterLearningResponseFull?> CreateCharacterLearningAsync(
        CreateCharacterLearningRequest request)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/CharacterLearnings", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CharacterLearningResponseFull>();
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

    public async Task<CharacterLearningResponseFull?> GetLearningAsync(string? id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/CharacterLearnings/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                return await response.Content.ReadFromJsonAsync<CharacterLearningResponseFull>();
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

    public async Task ToggleSkipStateAsync(string? id)
    {
        var response = await _httpClient.PatchAsJsonAsync($"api/CharacterLearnings/ToggleSkipState", id);

        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
        }
    }

    public async Task<RandomItemsInReviewResponse?> GetRandomMeaningsInReviewAsync(string characterId,
        Culture culture, int qty = 4)
    {
        try
        {
            string uri =
                $"api/CharacterLearnings/GetRandomMeaningsInReview?characterId={characterId}&culture={culture}&qty={qty}";
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                return await response.Content.ReadFromJsonAsync<RandomItemsInReviewResponse>();
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

    public async Task<RandomItemsInReviewResponse?> GetRandomKunReadingsInReviewAsync(string characterId,
        int qty = 4)
    {
        try
        {
            string uri =
                $"api/CharacterLearnings/GetRandomKunReadingsInReview?characterId={characterId}&qty={qty}";
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                return await response.Content.ReadFromJsonAsync<RandomItemsInReviewResponse>();
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

    public async Task<RandomItemsInReviewResponse?> GetRandomOnReadingsInReviewAsync(string characterId,
        int qty = 4)
    {
        try
        {
            string uri =
                $"api/CharacterLearnings/GetRandomOnReadingsInReview?characterId={characterId}&qty={qty}";
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                return await response.Content.ReadFromJsonAsync<RandomItemsInReviewResponse>();
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

    public async Task<IEnumerable<CharacterLearningResponseBase?>?> ListReviewQueueAsync
    (
        int pageNumber = 0,
        int pageSize = 0
    )
    {
        HttpResponseMessage response =
            await _httpClient.GetAsync(
                $"api/CharacterLearnings/ReviewQueue?PageNumber={pageNumber}&PageSize={pageSize}");

        return await TryGetResponseContent(response);
    }

    public async Task<IEnumerable<CharacterLearningResponseBase?>?> ListFutureReviewQueueAsync
    (
        int pageNumber = 0,
        int pageSize = 0
    )
    {
        HttpResponseMessage response =
            await _httpClient.GetAsync(
                $"api/CharacterLearnings/FutureReviewQueue?PageNumber={pageNumber}&PageSize={pageSize}");

        return await TryGetResponseContent(response);
    }

    public async Task<List<CharacterLearningResponseBase>?> ListSkippedAsync(int pageNumber = 0,
        int pageSize = 0)
    {
        var response = await _httpClient.GetAsync(
            $"api/CharacterLearnings/Skipped?PageNumber={pageNumber}&PageSize={pageSize}");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return new List<CharacterLearningResponseBase>();

            return await response.Content.ReadFromJsonAsync<List<CharacterLearningResponseBase>>() ??
                   new List<CharacterLearningResponseBase>();
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
    }

    public async Task<CharacterLearningResponseBase?> GetNextInReviewQueueAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/CharacterLearnings/GetNextInReviewQueue");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                CharacterLearningResponseBase? characterLearning =
                    await response.Content.ReadFromJsonAsync<CharacterLearningResponseBase>();
                if (characterLearning != null)
                    await _appState.ReviewCharState.UpdateNextToReview(characterLearning);

                return characterLearning;
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

    private static async Task<IEnumerable<CharacterLearningResponseBase?>?> TryGetResponseContent
        (HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return Enumerable.Empty<CharacterLearningResponseBase>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<CharacterLearningResponseBase>>() ??
                   Enumerable.Empty<CharacterLearningResponseBase>();
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
    }
}