namespace DanilvarKanji.Domain.Shared.Enumerations;

public static class UserRole
{
    public const string User = "User";
    public const string Admin = "Admin";
    public const string SuperAdmin = "SuperAdmin";

    public static string[] Roles =
    {
        User, Admin, SuperAdmin
    };
}