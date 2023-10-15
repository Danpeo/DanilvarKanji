using DanilvarKanji.DTO;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterService
{
    Task<bool> CreateAsync(CharacterDto characterDto);
    Task<IEnumerable<CharacterDto>> ListAsync();
    Task<CharacterDto> GetAsync(string id);
    Task<bool> Exist(string id);
    Task<bool> UpdateAsync(string id, CharacterDto characterDto);
    Task<bool> DeleteAsync(string id);
    Task<bool> ReplaceAsync(string id, CharacterDto characterDto);
    Task<CharacterDto?> GetPartialAsync(string id, IEnumerable<string> fields);
    Task<bool> AnyExist();
}