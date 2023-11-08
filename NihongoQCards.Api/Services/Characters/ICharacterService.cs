using DanilvarKanji.Shared.DTO;
using DanilvarKanji.Shared.Models.Common;
using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterService
{
    Task<bool> CreateAsync(CharacterDto characterDto);
    Task<IEnumerable<CharacterDto>> ListAsync(PaginationParams? paginationParams);
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
}