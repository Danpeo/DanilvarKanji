using DanilvarKanji.Shared.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Validation.Validators;

public class LearningSettings : ValidatorBase<Domain.Shared.Params.LearningSettings>
{
    public LearningSettings(IStringLocalizer<App> loc)
    {
        RuleFor(x => x.QtyOfCharsForLearningForDay)
            .GreaterThan(0)
            .WithMessage($"{loc["ValueTooSmall"]}0");
    }
}