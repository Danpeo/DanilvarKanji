using DanilvarKanji.DTO;
using DanilvarKanji.Models;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterLearningManagementService
{
    Task<bool> AddCharacterLearning(CharacterLearningDto characterDto, AppUser appUser);
    Task<bool> UpdateCharacterLearning(int id, CharacterLearningDto characterDto);
    Task<IEnumerable<CharacterLearningDto>> GetAllAsync();
    Task<IEnumerable<CharacterLearningDto>> GetAllForUserAsync(AppUser? appUser);
    Task<CharacterLearningDto> GetForUserAsync(int id, AppUser? appUser);
    Task<CharacterLearningDto> GetAsync(int id);
    Task<bool> Exist(int id);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteForUserAsync(int id, AppUser appUser);
}