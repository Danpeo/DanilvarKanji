using DanilvarKanji.Data;
using DanilvarKanji.Data.Configuration;
using DanilvarKanji.Services.Common;
using DanilvarKanji.Shared.Models;
using DanilvarKanji.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DanilvarKanji.Services.Characters;

public class CharacterLearningService : Service<ApplicationDbContext>, ICharacterLearningService
{
    private readonly IOptionsSnapshot<CharacterLearningSettings> _optionsSnapshot;

    public CharacterLearningService(ApplicationDbContext context,
        IOptionsSnapshot<CharacterLearningSettings> optionsSnapshot) : base(context)
    {
        _optionsSnapshot = optionsSnapshot;
    }

    public async Task<bool> IncreaseLearningRateAsync(string id, AppUser appUser, float value) =>
        await UpdateLearningRateAsync(id, appUser, value);

    public async Task<bool> DecreaseLearningRateAsync(string id, AppUser appUser, float value) =>
        await UpdateLearningRateAsync(id, appUser, -value);

    private async Task<bool> UpdateLearningRateAsync(string id, AppUser appUser, float value)
    {
        CharacterLearning? characterLearning = await Context.CharacterLearnings
            .FirstOrDefaultAsync(x => x.Id == id && x.AppUser == appUser);

        if (CanChangeCharacterLearningProgress(characterLearning))
        {
            characterLearning.LearningProgress.Value += value;
            
            CheckIfLessThanMinLearningRate(characterLearning);

            if (IfLearned(characterLearning))
            {
                SetCharacterToLearned(characterLearning);
                CheckIfCompletelyLearned(value, characterLearning);
            }

            Context.CharacterLearnings.Update(characterLearning);
        }

        return await SaveAsync();
    }

    private void CheckIfCompletelyLearned(float value, CharacterLearning characterLearning)
    {
        if (value > 0)
            characterLearning.LearnedCount++;
        else
            characterLearning.LearnedCount = 0;

        if (characterLearning.LearnedCount >= _optionsSnapshot.Value.LearnedCountToCompletelyLearn)
            characterLearning.LearningState = LearningState.CompletelyLearned;
    }

    private void CheckIfLessThanMinLearningRate(CharacterLearning characterLearning)
    {
        if (characterLearning.LearningProgress.Value < _optionsSnapshot.Value.MinLearningRate)
            characterLearning.LearningProgress.Value = _optionsSnapshot.Value.MinLearningRate;
    }

    private void SetCharacterToLearned(CharacterLearning characterLearning)
    {
        characterLearning.LearningProgress.Value = _optionsSnapshot.Value.MinLearningRate;
        characterLearning.LearningState = LearningState.LearnedForSomeTime;
    }

    private bool CanChangeCharacterLearningProgress(CharacterLearning? characterLearning) =>
        characterLearning != null &&
        characterLearning.LearningProgress.Value <= _optionsSnapshot.Value.MaxLearningRate &&
        characterLearning.LearningProgress.Value >= _optionsSnapshot.Value.MinLearningRate;

    private bool IfLearned(CharacterLearning characterLearning) =>
        characterLearning.LearningProgress.Value >= _optionsSnapshot.Value.MaxLearningRate;
}