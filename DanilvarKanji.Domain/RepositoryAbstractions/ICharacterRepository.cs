using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface ICharacterRepository
{
    Task<CharacterDto> GetAsyncObsolete(string id);
    Task<bool> Exist(string id);
    Task<bool> DeleteAsync(string id);
    Task<bool> AnyExist();
    Task<IEnumerable<string>> GetKanjiMeaningsByPriority(string characterId, int takeQty,
        Culture culture);
    void Create(Character character);
    Task<IEnumerable<Character>> ListAsync(PaginationParams? paginationParams);
    Task<Character?> GetAsync(string id);
    Task<IEnumerable<Character>> SearchAsync(string searchTerm);
    Task<IEnumerable<Character>> ListChildCharacters(string characterId);
    Task UpdateAsync(string id, Character character);
}