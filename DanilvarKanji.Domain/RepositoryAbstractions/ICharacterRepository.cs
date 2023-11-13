using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface ICharacterRepository
{
    Task<bool> CreateAsyncObsolete(CharacterDto characterDto);
    Task<IEnumerable<CharacterDto>> ListAsyncObsolete(PaginationParams? paginationParams);
    Task<CharacterDto> GetAsync(string id);
    Task<bool> Exist(string id);
    Task<bool> UpdateAsync(string id, CharacterDto characterDto);
    Task<bool> DeleteAsync(string id);
    Task<bool> ReplaceAsync(string id, CharacterDto characterDto);
    Task<CharacterDto?> GetPartialAsync(string id, IEnumerable<string> fields);
    Task<bool> AnyExist();

    Task<IEnumerable<string>> GetKanjiMeaningsByPriority(string characterId, int takeQty,
        Culture culture);

    Task<IEnumerable<CharacterDto>> ListChildCharacters(string id);
    Task<IEnumerable<CharacterDto>> SearchAsync(string searchTerm, PaginationParams? paginationParams);
    void CreateAsync(Character character);
    Task<IEnumerable<Character>> ListAsync(PaginationParams? paginationParams);
}