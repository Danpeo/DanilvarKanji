using DanilvarKanji.DTO;
using DanilvarKanji.Models;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterLearningService
{
    Task<bool> UpdateCharacterLearningState(CharacterForLearnDto characterDto, AppUser appUser);
}