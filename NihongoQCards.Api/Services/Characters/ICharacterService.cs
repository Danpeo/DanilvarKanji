using DanilvarKanji.DTO;
using DanilvarKanji.Models;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterService
{
    Task<bool> CreateAsync(CharacterDto characterDto);
    Task<IEnumerable<CharacterDto>> GetAllAsync();
    Task<CharacterDto> GetAsync(int id);
    bool Exist(int id);
    Task<bool> UpdateAsync(int id, CharacterDto characterDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> CreateAsync(Character characterDto);
}