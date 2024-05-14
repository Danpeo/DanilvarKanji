using DanilvarKanji.Domain.Primitives;

namespace DanilvarKanji.Domain.Errors;

public static class User
{
    public static Error NotFound =>
        new("User.NotFound", "The user with the specified identifier was not found.");

    public static Error InvalidPermissions =>
        new(
            "User.InvalidPermissions",
            "The current user does not have the permissions to perform that operation."
        );

    public static Error DuplicateEmail =>
        new("User.DuplicateEmail", "The specified email is already in use.");

    public static Error PasswordInputsDontMatch =>
        new("User.PasswordInputsDontMatch", "Password inputs should be the same.");

    public static Error CannotChangePassword =>
        new("User.CannotChangePassword", "The password cannot be changed to the specified password.");

    public static Error WrongCredentials =>
        new("User.WrongCredentials", "Login Creadentials are not valid.");

    public static Error EmailNotComfirmed =>
        new("User.EmailNotComfirmed", "Email for this user was not confirmed");
}