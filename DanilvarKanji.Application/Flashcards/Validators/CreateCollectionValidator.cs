using DanilvarKanji.Application.Flashcards.Commands;
using DanilvarKanji.Shared.Validation;
using FluentValidation;

namespace DanilvarKanji.Application.Flashcards.Validators;

public class CreateCollectionValidator : ValidatorBase<CreateFlashcardCollectionCommand>
{
  public CreateCollectionValidator()
  {
    RuleFor(c => c.Name).NotEmpty().WithMessage("Name should have a value.");

    RuleFor(c => c.Flashcards)
      .NotNull()
      .WithMessage("Flashcards cannot be NULL.")
      .Must(HaveCollectionWithAtLeastOneElement)
      .WithMessage("Flashcards must have at least one element");

    RuleForEach(c => c.Flashcards).SetValidator(new FlashcardValidator());
  }
}