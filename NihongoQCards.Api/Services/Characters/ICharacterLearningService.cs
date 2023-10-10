using DanilvarKanji.Models;

namespace DanilvarKanji.Services.Characters;

public interface ICharacterLearningService
{
    Task<bool> IncreaseLearningRateAsync(int id, AppUser appUser, float value);
    Task<bool> DecreaseLearningRateAsync(int id, AppUser appUser, float value);
}