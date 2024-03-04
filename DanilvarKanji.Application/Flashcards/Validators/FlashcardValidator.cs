using DanilvarKanji.Shared.Domain.Entities.Flashcards;
using DanilvarKanji.Shared.Validation;
using FluentValidation;

namespace DanilvarKanji.Application.Flashcards.Validators;

public class FlashcardValidator : ValidatorBase<Flashcard>
{
    public FlashcardValidator()
    {
        RuleFor(f => f.Main)
            .NotNull()
            .NotEmpty()
            .WithMessage("Main should have a value");

        RuleFor(f => f.Back)
            .NotNull()
            .NotEmpty()
            .WithMessage("Back should have a value");

        RuleFor(f => f.RememberedInARow)
            .GreaterThan(0)
            .WithMessage("RememberedInARow should be greater than 0.");
    }
}