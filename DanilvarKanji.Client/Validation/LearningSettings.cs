using DanilvarKanji.Shared.Domain.Params;
using DanilvarKanji.Shared.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Validation;

public class LearningSettings : ValidatorBase<DanilvarKanji.Shared.Domain.Params.LearningSettings>
{
    public LearningSettings(IStringLocalizer<App> loc)
    {
        RuleFor(x => x.QtyOfCharsForLearningForDay)
            .GreaterThan(0)
            .WithMessage($"{loc["ValueTooSmall"]}0");
    }
}