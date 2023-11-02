using DanilvarKanji.Client.Localization.LocaleKeys;
using DanilvarKanji.Shared.DTO;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Validation;

public class CharacterValidator : AbstractValidator<CharacterDto>
{
    public CharacterValidator(IStringLocalizer<App> localizer)
    {
        RuleFor(x => x.Definition)
            .NotEmpty()
            .WithMessage(localizer[nameof(AppLocaleKeys.NotEmptyCharDef)])
            .MaximumLength(1)
            .WithMessage($"{localizer[nameof(AppLocaleKeys.ValueTooLong)]}1");
    }
}