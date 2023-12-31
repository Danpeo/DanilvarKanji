using DanilvarKanji.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Validation;

public class StringDefinitionValidator : AbstractValidator<StringDefinition>
{
    public StringDefinitionValidator(IStringLocalizer<App> localizer)
    {
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage(localizer["NotEmptyValue"]);
    }
}