using DanilvarKanji.Client.Localization.LocaleKeys;
using DanilvarKanji.Shared.Requests.Characters;
using DanilvarKanji.Shared.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Validation;

// ReSharper disable once UnusedType.Global
public class AddCharacterValidator : BaseCharacterValidator<CreateCharacterRequest>
{
    public AddCharacterValidator(IStringLocalizer<App> localizer)
    {
        RuleFor(x => x.Definition)
            .NotEmpty()
            .WithMessage(localizer[nameof(AppLocaleKeys.NotEmptyCharDef)])
            .Must(StrIsJapanese);

        RuleFor(x => x.StrokeCount)
            .NotEmpty()
            .WithMessage(localizer[nameof(AppLocaleKeys.NotEmptyStrokeCount)])
            .GreaterThan(0)
            .WithMessage($"{localizer[nameof(AppLocaleKeys.ValueTooLong)]}0");

        RuleFor(x => x.Mnemonics)
            .NotEmpty()
            .WithMessage(localizer[nameof(AppLocaleKeys.NoMnemonics)])
            .NotNull()
            .WithMessage(localizer[nameof(AppLocaleKeys.NoMnemonics)])
            .Must(HaveStringDefinitionInAllCultures);

        RuleFor(x => x.KanjiMeanings)
            .NotEmpty()
            .WithMessage(localizer["NoMeanings"])
            .NotNull()
            .WithMessage(localizer["NoMeanings"])
            .Must(HaveMeaningsInAllCultures);

        RuleFor(x => x.Kunyomis)
            .Must(HaveAtLeastOneCollection)
            .When(x => x.Onyomis.Count == 0)
            .WithMessage(localizer["NotEmpty"]);

        RuleFor(x => x.Onyomis)
            .Must(HaveAtLeastOneCollection)
            .When(x => x.Kunyomis.Count == 0)
            .WithMessage(localizer["NotEmpty"]);
    }
}