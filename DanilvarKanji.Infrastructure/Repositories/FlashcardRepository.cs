using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Common;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Entities.Flashcards;
using DanilvarKanji.Shared.Domain.Params;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Repositories;

public class FlashcardRepository : IFlashcardRepository
{
    private readonly ApplicationDbContext _context;

    public FlashcardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateCollection(FlashcardCollection collection) =>
        _context.FlashcardCollections.Add(collection);

    public async Task AddFlashcardToCollectionAsync(string collectionId, AppUser user, Flashcard flashcard)
    {
        FlashcardCollection? collection = await _context.FlashcardCollections
            .FirstOrDefaultAsync(c => c.Id == collectionId && c.AppUser == user);

        collection?.Flashcards.Add(flashcard);
    }

    public async Task<IEnumerable<FlashcardCollection>> ListCollectionsAsync(PaginationParams? paginationParams,
        AppUser user)
    {
        var collections = await _context.FlashcardCollections
            .Include(c => c.Flashcards)
            .Where(c => c.AppUser == user)
            .ToListAsync();

        return paginationParams != null ? Paginator.Paginate(collections, paginationParams) : collections;
    }

    public async ValueTask<bool> AnyCollectionsExistAsync(AppUser user) =>
        await _context.FlashcardCollections.AnyAsync(fc => fc.AppUser == user);
}