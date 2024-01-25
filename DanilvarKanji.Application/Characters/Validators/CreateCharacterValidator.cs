using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Enumerations;
using FluentValidation;

namespace DanilvarKanji.Application.Characters.Validators;

// ReSharper disable once UnusedType.Global
public class CreateCharacterValidator : AbstractValidator<CreateCharacterCommand>
{
    public CreateCharacterValidator()
    {
        RuleFor(x => x.Definition)
            .NotEmpty()
            .WithMessage("Definition should have a value.");

        RuleFor(x => x.StrokeCount)
            .NotEmpty()
            .WithMessage("Stroke cound should have a value")
            .GreaterThan(0)
            .WithMessage("Stroke Count should be greater than 0.");

        RuleFor(x => x.Mnemonics)
            .NotEmpty()
            .WithMessage("Mnemonics should have a value.")
            .NotNull()
            .WithMessage("Mnemonics should not be NULL")
            .Must(HaveStringDefinitionInAllCultures)
            .WithMessage("Mnemonics must have values in both cultures.");

        RuleFor(x => x.KanjiMeanings)
            .NotEmpty()
            .WithMessage("Kanji Meanings should have a value.")
            .NotNull()
            .WithMessage("Kanji Meanings should not be NULL.")
            .Must(HaveMeaningsInAllCultures)
            .WithMessage("KanjiMeanings must have values in both cultures.");

        RuleFor(x => x.Kunyomis)
            .Must(HaveAtLeastOneCollection)
            .When(x => x.Onyomis == null || x.Onyomis.Count == 0)
            .WithMessage("Either Kunyomis or Onyomis must be provided.");

        RuleFor(x => x.Onyomis)
            .Must(HaveAtLeastOneCollection)
            .When(x => x.Kunyomis == null || x.Kunyomis.Count == 0)
            .WithMessage("Either Kunyomis or Onyomis must be provided.");
    }

    private static bool HaveAtLeastOneCollection<T>(ICollection<T>? collection)
    {
        return collection is { Count: > 0 };
    }

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