using DanilvarKanji.Shared.Requests.Flashcards;
using DanilvarKanji.Shared.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Validation;

public class AddFlashcardCollectionValidator : ValidatorBase<CreateFlashcardCollectionRequest>
{
    public AddFlashcardCollectionValidator(IStringLocalizer<App> localizer)
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name should have a value.");
        
        RuleFor(c => c.Flashcards)
            .NotNull()
            .WithMessage("Flashcards cannot be NULL.")
            .Must(HaveCollectionWithAtLeastOneElement)
            .WithMessage("Flashcards must have at least one element");
    }
}