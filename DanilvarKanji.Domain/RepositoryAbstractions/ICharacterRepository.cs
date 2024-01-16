using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface ICharacterRepository
{
    Task<bool> Exist(string id);
    Task DeleteAsync(string id);
    Task<bool> AnyExist();
    Task<IEnumerable<string>> GetKanjiMeaningsByPriority(string characterId, int takeQty,
        Culture culture);
    void Create(Character character);
    Task<IEnumerable<Character>> ListAsync(PaginationParams? paginationParams);
    Task<Character?> GetAsync(string id);
    Task<IEnumerable<Character>> SearchAsync(string searchTerm);
    Task<IEnumerable<Character>> ListChildCharacters(string characterId);
    Task UpdateAsync(string id, Character character);
    Task<IEnumerable<Character>> ListLearnQueueAsync(PaginationParams? paginationParams, AppUser user,
        JlptLevel jlptLevel = JlptLevel.N5);

    Task<Character?> GetNextInLearnQueueAsync(AppUser user);
    Task<bool> AnyInLearnQueue(AppUser user);
    Task<List<string>> GetRandomMeaningsInLearnQueueAsync(AppUser user, Culture culture, int qty);
    string? GetRandomMeaningFromCharacter(string id, Culture culture);
    void DeleteRange(IEnumerable<string> ids);
    void DeleteAll();
    string? GetRandomKunyomiFromCharacter(string id);
    string? GetRandomOnyomiFromCharacter(string id);
    Task<List<string>> GetRandomKunReadingsInLearnQueueAsync(AppUser user, int qty);
    Task<List<string>> GetRandomOnReadingsInLearnQueueAsync(AppUser user, int qty);
}