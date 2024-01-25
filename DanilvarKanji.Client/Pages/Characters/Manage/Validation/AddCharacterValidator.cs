using DanilvarKanji.Client.Localization.LocaleKeys;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Requests.Characters;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Pages.Characters.Manage.Validation;

// ReSharper disable once UnusedType.Global
public class AddCharacterValidator : AbstractValidator<CreateCharacterRequest>
{
    public AddCharacterValidator(IStringLocalizer<App> localizer)
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

    private static bool HaveAtLeastOneCollection<T>(ICollection<T>? collection) => collection is { Count: > 0 };

    private static bool HaveMeaningsInAllCultures(ICollection<KanjiMeaning>? kanjiMeanings)
    {
        if (kanjiMeanings == null || kanjiMeanings.Count < 2)
            return false;

        return kanjiMeanings
                   .Any(meaning => meaning.Definitions!
                       .Any(def => def.Culture == Culture.EnUS)) &&
               kanjiMeanings
                   .Any(meaning => meaning.Definitions!
                       .Any(def => def.Culture == Culture.RuRU));
    }

    private static bool HaveStringDefinitionInAllCultures(ICollection<StringDefinition>? defs)
    {
        if (defs == null || defs.Count < 2)
            return false;

        return defs.Any(d => d.Culture == Culture.EnUS) &&
               defs.Any(d => d.Culture == Culture.RuRU);
    }
}