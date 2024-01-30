using DanilvarKanji.Application.Users.Commands;
using DanilvarKanji.Shared.Validation;
using FluentValidation;

namespace DanilvarKanji.Application.Users.Validators;

public class UpdateUserLearningSettingsValidator : BaseValidator<UpdateUserLearningSettingsCommand>
{
    public UpdateUserLearningSettingsValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email should have a value.")
            .NotNull()
            .WithMessage("Email should have a value.");

        RuleFor(x => x.LearningSettings)
            .NotNull()
            .WithMessage("Learning Settings should not be NULL.");
        
        RuleFor(x => x.LearningSettings.QtyOfCharsForLearningForDay)
            .NotEmpty()
            .WithMessage("Qty of characters for learning for day should have a value.")
            .GreaterThan(0)
            .WithMessage("Qty of characters for learning for day should be greater than 0.");
    }
}