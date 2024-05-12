using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Domain.Errors;

public static class Identity
{
  public static IdentityError EmailNotUnique =>
    new() { Code = "Identity.EmailNotUnique", Description = "Email is alread taken." };

  public static IdentityError PasswordsDontMatch =>
    new() { Code = "Identity.PasswordsDontMatch", Description = "Password inputs don't match." };

  public static IdentityError NotFound =>
    new() { Code = "Identity.NotFound", Description = "User not found." };

  public static IdentityError EmailAlreadyConfirmed =>
    new()
    {
      Code = "Identity.EmailAlreadyConfirmed",
      Description = "Email for specified user was already confirmed."
    };

  public static IdentityError ConfirmationCodeIsNotValid =>
    new()
    {
      Code = "Identity.ConfirmationCodeIsNotValid",
      Description = "Confirmation code is not valid."
    };
}