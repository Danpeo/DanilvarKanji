using DanilvarKanji.DTO;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterService
{
    Task<bool> CreateAsync(CharacterDto characterDto);
    Task<IEnumerable<CharacterDto>> GetAllAsync();
    Task<CharacterDto> GetAsync(Guid id);
    Task<bool> Exist(Guid id);
    Task<bool> UpdateAsync(Guid id, CharacterDto characterDto);
    Task<bool> DeleteAsync(Guid id);
}