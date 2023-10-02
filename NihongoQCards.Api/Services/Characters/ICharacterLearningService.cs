using DanilvarKanji.DTO;
using DanilvarKanji.Models;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterLearningService
{
    Task<bool> AddCharacterLearning(CharacterForLearnDto characterDto, AppUser appUser);
    Task<bool> UpdateCharacterLearning(int id, CharacterForLearnDto characterDto);
    Task<IEnumerable<CharacterForLearnDto>> GetAllAsync();
    Task<IEnumerable<CharacterForLearnDto>> GetAllForUserAsync(AppUser? appUser);
    Task<CharacterForLearnDto> GetForUserAsync(int id, AppUser? appUser);
    Task<CharacterForLearnDto> GetAsync(int id);
    Task<bool> Exist(int id);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteForUserAsync(int id, AppUser appUser);
}