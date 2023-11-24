using DanilvarKanji.Shared.Requests.Auth;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Pages.Auth.Validation;

// ReSharper disable once UnusedType.Global
public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator(IStringLocalizer<App> localizer)
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(localizer["NotEmpty"]);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(localizer["NotEmpty"])
            .EmailAddress()
            .WithMessage(localizer["InvalidEmail"]);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(localizer["NotEmpty"])
            .MinimumLength(8)
            .WithMessage($"{localizer["PasswordTooShort"]} 8")
            .Matches("[0-9]")
            .WithMessage(localizer["PasswordRequiresDigit"])
            .Matches("[A-Z]")
            .WithMessage(localizer["PasswordRequiresUpper"])
            .Matches("[a-z]")
            .WithMessage(localizer["PasswordRequiresLower"])
            .Matches("[^a-zA-Z0-9]")
            .WithMessage(localizer["PasswordRequiresNonAlphanumeric"]);

        RuleFor(x => x.PasswordRepeat)
            .NotEmpty()
            .WithMessage(localizer["NotEmpty"])
            .Equal(x => x.Password)
            .WithMessage(localizer["PasswordRepeatMismath"]);
    }
}