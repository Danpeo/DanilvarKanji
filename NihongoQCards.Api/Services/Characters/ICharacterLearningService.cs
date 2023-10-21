using DanilvarKanji.Shared.Models;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterLearningService
{
    Task<bool> IncreaseLearningRateAsync(string id, AppUser appUser, float value);
    Task<bool> DecreaseLearningRateAsync(string id, AppUser appUser, float value);
}