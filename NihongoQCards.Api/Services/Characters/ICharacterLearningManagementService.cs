using DanilvarKanji.DTO;
using DanilvarKanji.Models;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterLearningManagementService
{
    Task<bool> AddCharacterLearning(CharacterLearningDto characterDto, AppUser appUser);
    Task<bool> UpdateCharacterLearning(Guid id, CharacterLearningDto characterDto);
    Task<IEnumerable<CharacterLearningDto>> GetAllAsync();
    Task<IEnumerable<CharacterLearningDto>> GetAllForUserAsync(AppUser? appUser);
    Task<CharacterLearningDto> GetForUserAsync(Guid id, AppUser? appUser);
    Task<CharacterLearningDto> GetAsync(Guid id);
    Task<bool> Exist(Guid id);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> DeleteForUserAsync(Guid id, AppUser appUser);
}