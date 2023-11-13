using DanilvarKanji.Client.Localization.LocaleKeys;
using DanilvarKanji.Domain.DTO;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Validation;

public class CharacterValidator : AbstractValidator<CharacterDto>
{
    public CharacterValidator(IStringLocalizer<App> localizer)
    {
        RuleFor(x => x.Definition)
            .NotEmpty()
            .WithMessage(localizer[nameof(AppLocaleKeys.NotEmptyCharDef)]);
        
        RuleFor(x => x.StrokeCount)
            .NotEmpty()
            .WithMessage(localizer[nameof(AppLocaleKeys.NotEmptyStrokeCount)])
            .GreaterThan(0)
            .WithMessage($"{localizer[nameof(AppLocaleKeys.ValueTooLong)]}0");

        RuleFor(x => x.Mnemonics)
            .NotEmpty()
            .WithMessage(localizer[nameof(AppLocaleKeys.NoMnemonics)])
            .NotNull()
            .WithMessage(localizer[nameof(AppLocaleKeys.NoMnemonics)]);
        
        RuleFor(x => x.KanjiMeanings)
            .NotEmpty()
            .WithMessage(localizer["NoMeanings"])
            .NotNull()
            .WithMessage(localizer["NoMeanings"]);
    }
}