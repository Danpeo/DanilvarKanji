using System.Net;
using System.Net.Http.Json;
using DanilvarKanji.Shared.Domain.Entities.Flashcards;
using DanilvarKanji.Shared.Requests.Flashcards;
using DanilvarKanji.Shared.Responses.Flashcards;

namespace DanilvarKanji.Client.Services.Flashcards;

public class FlashcardApiService : IFlashcardApiService
{
    private readonly HttpClient _httpClient;

    public FlashcardApiService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ServerApi");
    }

    public async Task CreateFlashcardCollectionAsync(CreateFlashcardCollectionRequest request) =>
        await Http.PostAsync(request, "api/Flashcards/Collection", _httpClient);

    public async Task UpdateFlashcardCollectionAsync(UpdateFlashcardCollectionRequest request) =>
        await Http.PutAsync(request, "api/Flashcards/Collection", _httpClient);

    public async Task<List<FlashcardCollectionResponse>?> ListFlashcardCollectionsAsync(int pageNumber = 0,
        int pageSize = 0)
    {
        string requestUri = $"api/Flashcards/Collections?PageNumber={pageNumber}&PageSize={pageSize}";
        return await Http.ListAsync<FlashcardCollectionResponse>(requestUri, _httpClient);
    }

    public async Task<FlashcardCollectionResponse?> GetFlashcardsCollectionAsync(string id)
    {
        string requestUri = $"api/Flashcards/Collection/{id}";
        return await Http.GetAsync<FlashcardCollectionResponse>(requestUri, _httpClient);
        /*HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return default;

            return await response.Content.ReadFromJsonAsync<FlashcardCollection>();
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");*/
    }
}