using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Entities.Flashcards;
using DanilvarKanji.Shared.Domain.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface IFlashcardRepository
{
    void CreateCollection(FlashcardCollection collection);
    Task AddFlashcardToCollectionAsync(string collectionId, AppUser user, Flashcard flashcard);

    Task<IEnumerable<FlashcardCollection>> ListCollectionsAsync(PaginationParams? paginationParams,
        AppUser user);

    ValueTask<bool> AnyCollectionsExistAsync(AppUser user);
}