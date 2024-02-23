using DanilvarKanji.Shared.Requests.Flashcards;

namespace DanilvarKanji.Client.Services.Flashcards;

public class FlashcardApiService : ApiService, IFlashcardApiService
{
    private readonly HttpClient _httpClient;

    public FlashcardApiService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ServerApi");
    }

    public async Task CreateFlashcardCollectionAsync(CreateFlashcardCollectionRequest request) => 
        await PostAsync(request, "api/Flashcards/Collection", _httpClient);
}