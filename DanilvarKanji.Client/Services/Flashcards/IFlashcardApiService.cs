using DanilvarKanji.Shared.Requests.Flashcards;
using DanilvarKanji.Shared.Responses.Flashcards;

namespace DanilvarKanji.Client.Services.Flashcards;

public interface IFlashcardApiService
{
  Task CreateFlashcardCollectionAsync(CreateFlashcardCollectionRequest request);

  Task<List<FlashcardCollectionResponse>?> ListFlashcardCollectionsAsync(
    int pageNumber = 0,
    int pageSize = 0
  );

  Task<FlashcardCollectionResponse?> GetFlashcardsCollectionAsync(string id);
  Task UpdateFlashcardCollectionAsync(UpdateFlashcardCollectionRequest request);
  Task DeleteCollectionAsync(string id);
}