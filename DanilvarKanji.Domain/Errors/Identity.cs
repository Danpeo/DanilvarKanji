using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Domain.Errors;

public static class Identity
{
    public static IdentityError EmailNotUnique => new()
    {
        Code = "EmailNotUnique",
        Description = "Email is alread taken."
    };

    public static IdentityError PasswordsDontMatch => new()
    {
        Code = "PasswordsDontMatch",
        Description = "Password inputs don't match."
    };
}