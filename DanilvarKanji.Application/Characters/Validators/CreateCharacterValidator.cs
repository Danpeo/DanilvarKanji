using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Shared.Validation;
using FluentValidation;

namespace DanilvarKanji.Application.Characters.Validators;

// ReSharper disable once UnusedType.Global
public class CreateCharacterValidator : CharacterValidatorBase<CreateCharacterCommand>
{
    public CreateCharacterValidator()
    {
        RuleFor(x => x.Definition)
            .NotEmpty()
            .WithMessage("Definition should have a value.")
            .Must(StrIsJapanese)
            .WithMessage("Definition should be in Japanese.");

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
            .Must(HaveCollectionWithAtLeastOneElement)
            .When(x => x.Onyomis == null || x.Onyomis.Count == 0)
            .WithMessage("Either Kunyomis or Onyomis must be provided.");

        RuleFor(x => x.Onyomis)
            .Must(HaveCollectionWithAtLeastOneElement)
            .When(x => x.Kunyomis == null || x.Kunyomis.Count == 0)
            .WithMessage("Either Kunyomis or Onyomis must be provided.");
    }
}