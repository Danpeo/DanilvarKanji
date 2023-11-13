using DanilvarKanji.Data.Configuration;
using DanilvarKanji.Domain.DTO;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace DanilvarKanji.Validators;

public class CharacterLearningValidator : AbstractValidator<CharacterLearningDto>
{
    private readonly IOptionsSnapshot<CharacterLearningSettings> _optionsSnapshot;

    public CharacterLearningValidator(IOptionsSnapshot<CharacterLearningSettings> optionsSnapshot)
    {
        _optionsSnapshot = optionsSnapshot;

        SetRules();
    }

    private void SetRules()
    {
        RuleFor(x => x.LearningProgress)
            .InclusiveBetween(_optionsSnapshot.Value.MinLearningRate, _optionsSnapshot.Value.MaxLearningRate)
            .WithMessage(
                $"Learning Progress must be between {_optionsSnapshot.Value.MinLearningRate} and {_optionsSnapshot.Value.MaxLearningRate}.");

        RuleFor(x => x.LearnedCount)
            .InclusiveBetween(0, _optionsSnapshot.Value.LearnedCountToCompletelyLearn)
            .WithMessage($"Learned Count must be between 0 and {_optionsSnapshot.Value.LearnedCountToCompletelyLearn}");
    }
}