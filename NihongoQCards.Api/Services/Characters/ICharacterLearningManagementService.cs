using DanilvarKanji.Shared.DTO;
using DanilvarKanji.Shared.Models;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterLearningManagementService
{
    Task<bool> CreateAsync(CharacterLearningDto characterDto, AppUser appUser);
    Task<bool> UpdateAsync(string id, CharacterLearningDto characterDto);
    Task<IEnumerable<CharacterLearningDto>> ListAsync();
    Task<IEnumerable<CharacterLearningDto>> ListForUserAsync(AppUser? appUser);
    Task<CharacterLearningDto> GetForUserAsync(string id, AppUser? appUser);
    Task<CharacterLearningDto> GetAsync(string id);
    Task<bool> Exist(string id);
    Task<bool> DeleteAsync(string id);
    Task<bool> DeleteForUserAsync(string id, AppUser appUser);
    Task<bool> AnyExist();
    Task<bool> AnyExist(AppUser? appUser);
    Task<bool> ReplaceAsync(string id, CharacterLearningDto characterDto);
}