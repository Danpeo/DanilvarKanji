using DanilvarKanji.DTO;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterService
{
    Task<bool> CreateAsync(CharacterDto characterDto);
    Task<IEnumerable<CharacterDto>> GetAllAsync();
    Task<CharacterDto> GetAsync(int id);
    Task<bool> Exist(int id);
    Task<bool> UpdateAsync(int id, CharacterDto characterDto);
    Task<bool> DeleteAsync(int id);
}