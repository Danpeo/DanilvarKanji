using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Entities.Flashcards;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Infrastructure.Common;
using DanilvarKanji.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Repositories;

public class FlashcardRepository : IFlashcardRepository
{
    private readonly ApplicationDbContext _context;

    public FlashcardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateCollection(FlashcardCollection collection)
    {
        _context.FlashcardCollections
            .Add(collection);
    }

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

    public async Task UpdateCollectionAsync(string id, AppUser appUser, FlashcardCollection newCollection)
    {
        var collection = await GetCollectionAsync(id, appUser);

        if (collection is not null)
        {
            collection.Name = newCollection.Name;
            collection.Flashcards = newCollection.Flashcards;
            _context.FlashcardCollections.Update(collection);
        }
    }

    public async Task<FlashcardCollection?> GetCollectionAsync(string id, AppUser user) =>
        await _context.FlashcardCollections
            .Include(c => c.Flashcards)
            .Where(fc => fc.Id == id && fc.AppUser == user)
            .FirstOrDefaultAsync();

    public async Task DeleteAsync(string id, AppUser appUser)
    {
        var collection = await GetCollectionAsync(id, appUser);
        _context.FlashcardCollections.Remove(collection!);
    }
    
    public async ValueTask<bool> AnyCollectionsExistAsync(AppUser user) =>
        await _context.FlashcardCollections.AnyAsync(fc => fc.AppUser == user);

    public async ValueTask<bool> ExistAsync(string id, AppUser user) =>
        await _context.FlashcardCollections.AnyAsync(s => s.Id == id && s.AppUser == user);
}