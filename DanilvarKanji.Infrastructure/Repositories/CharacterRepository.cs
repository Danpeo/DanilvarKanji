using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly ApplicationDbContext _context;

    public CharacterRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<bool> CreateAsyncObsolete(CharacterDto characterDto)
    {
        throw new NotImplementedException();
    }

    public void CreateAsync(Character character) =>
        _context.Characters.Add(character);

    public async Task<IEnumerable<CharacterDto>> ListAsyncObsolete(PaginationParams? paginationParams)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Character>> ListAsync(PaginationParams? paginationParams) =>
        await GetCharactersWithRelatedData(paginationParams)
            .ToListAsync();

    public Task<CharacterDto> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Exist(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(string id, CharacterDto characterDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ReplaceAsync(string id, CharacterDto characterDto)
    {
        throw new NotImplementedException();
    }

    public Task<CharacterDto?> GetPartialAsync(string id, IEnumerable<string> fields)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyExist() => 
        _context.Characters.AnyAsync();

    public Task<IEnumerable<string>> GetKanjiMeaningsByPriority(string characterId, int takeQty, Culture culture)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CharacterDto>> ListChildCharacters(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CharacterDto>> SearchAsync(string searchTerm, PaginationParams? paginationParams)
    {
        throw new NotImplementedException();
    }

    private IQueryable<Character> GetCharactersWithRelatedData(PaginationParams? paginationParams = null)
    {
        var characters = _context.Characters
            .Include(x => x.Mnemonics)
            .Include(x => x.KanjiMeanings)
            .ThenInclude(x => x.Definitions)
            .Include(x => x.Kunyomis)
            .Include(x => x.Onyomis)
            .Include(x => x.Words)
            .ThenInclude(x => x.WordMeanings);

        if (paginationParams != null && paginationParams.PageNumber != 0 && paginationParams.PageSize != 0)
        {
            return characters
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);
        }

        return characters;
    }
}