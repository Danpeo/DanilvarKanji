using DanilvarKanji.Shared.Requests.Auth;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Pages.Auth.Validation;

// ReSharper disable once UnusedType.Global
public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
  public LoginUserValidator(IStringLocalizer<App> localizer)
  {
    RuleFor(x => x.Email).NotEmpty().WithMessage(localizer["NotEmpty"]);

    RuleFor(x => x.Password).NotEmpty().WithMessage(localizer["NotEmpty"]);
  }
}