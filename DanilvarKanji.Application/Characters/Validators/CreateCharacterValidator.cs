using DanilvarKanji.Application.Characters.Commands;
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
            .WithMessage("Mnemonics should not be NULL");
        
        RuleFor(x => x.KanjiMeanings)
            .NotEmpty()
            .WithMessage("Kanji Meanings should have a value.")
            .NotNull()
            .WithMessage("Kanji Meanings should not be NULL");
    }
}