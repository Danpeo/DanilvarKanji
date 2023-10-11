using DanilvarKanji.Models;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterLearningService
{
    Task<bool> IncreaseLearningRateAsync(Guid id, AppUser appUser, float value);
    Task<bool> DecreaseLearningRateAsync(Guid id, AppUser appUser, float value);
}