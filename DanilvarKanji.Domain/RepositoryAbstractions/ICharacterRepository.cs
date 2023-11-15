using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface ICharacterRepository
{
    Task<bool> CreateAsyncObsolete(CharacterDto characterDto);
    Task<IEnumerable<CharacterDto>> ListAsyncObsolete(PaginationParams? paginationParams);
    Task<CharacterDto> GetAsyncObsolete(string id);
    Task<bool> Exist(string id);
    Task<bool> UpdateAsyncObsolete(string id, CharacterDto characterDto);
    Task<bool> DeleteAsync(string id);
    Task<bool> ReplaceAsync(string id, CharacterDto characterDto);
    Task<CharacterDto?> GetPartialAsync(string id, IEnumerable<string> fields);
    Task<bool> AnyExist();

    Task<IEnumerable<string>> GetKanjiMeaningsByPriority(string characterId, int takeQty,
        Culture culture);

    Task<IEnumerable<CharacterDto>> ListChildCharactersObsolete(string id);
    Task<IEnumerable<CharacterDto>> SearchAsyncObsolete(string searchTerm, PaginationParams? paginationParams);
    void CreateAsync(Character character);
    Task<IEnumerable<Character>> ListAsync(PaginationParams? paginationParams);
    Task<Character?> GetAsync(string id);
    Task<IEnumerable<Character>> SearchAsync(string searchTerm);
    Task<IEnumerable<Character>> ListChildCharacters(string characterId);
    Task UpdateAsync(string id, Character character);
}