using DanilvarKanji.Shared.Requests.Flashcards;

namespace DanilvarKanji.Client.Services.Flashcards;

public interface IFlashcardApiService
{
    Task CreateFlashcardCollectionAsync(CreateFlashcardCollectionRequest request);
}