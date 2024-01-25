using System.Net;
using System.Net.Http.Json;
using DanilvarKanji.Client.State;
using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Requests.CharacterLearnings;
using DanilvarKanji.Shared.Responses.CharacterLearning;

namespace DanilvarKanji.Client.Services.Characters;

public class CharacterLearningService : ICharacterLearningService
{
    private readonly HttpClient _httpClient;
    private readonly AppState _appState;

    public CharacterLearningService(IHttpClientFactory factory, AppState appState)
    {
        _appState = appState;
        _httpClient = factory.CreateClient("ServerApi");
    }

    public async Task<GetAllFromCharacterLearningResponse?> CreateCharacterLearningAsync(
        CreateCharacterLearningRequest request)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/CharacterLearnings", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GetAllFromCharacterLearningResponse>();
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

    public async Task<GetAllFromCharacterLearningResponse?> GetLearningAsync(string? id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/CharacterLearnings/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                return await response.Content.ReadFromJsonAsync<GetAllFromCharacterLearningResponse>();
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

    public async Task<GetRandomItemsInReviewResponse?> GetRandomMeaningsInReviewAsync(string characterId,
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

                return await response.Content.ReadFromJsonAsync<GetRandomItemsInReviewResponse>();
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

    public async Task<GetRandomItemsInReviewResponse?> GetRandomKunReadingsInReviewAsync(string characterId,
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

                return await response.Content.ReadFromJsonAsync<GetRandomItemsInReviewResponse>();
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

    public async Task<GetRandomItemsInReviewResponse?> GetRandomOnReadingsInReviewAsync(string characterId,
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

                return await response.Content.ReadFromJsonAsync<GetRandomItemsInReviewResponse>();
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

    public async Task<IEnumerable<GetCharacterLearningBaseInfoResponse?>?> ListReviewQueueAsync(int pageNumber = 0,
        int pageSize = 0)
    {
        try
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync(
                    $"api/CharacterLearnings/ReviewQueue?PageNumber={pageNumber}&PageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return Enumerable.Empty<GetCharacterLearningBaseInfoResponse>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<GetCharacterLearningBaseInfoResponse>>() ??
                       Enumerable.Empty<GetCharacterLearningBaseInfoResponse>();
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

    public async Task<GetCharacterLearningBaseInfoResponse?> GetNextInReviewQueueAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/CharacterLearnings/GetNextInReviewQueue");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                GetCharacterLearningBaseInfoResponse? characterLearning =
                    await response.Content.ReadFromJsonAsync<GetCharacterLearningBaseInfoResponse>();
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
}