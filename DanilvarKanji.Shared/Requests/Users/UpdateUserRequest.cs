using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Shared.Requests.Users;

public class UpdateUserRequest
{
    public string Email { get; set; }
    public string NewUserName { get; set; }
    public string NewUserRole { get; set; }

    public UpdateUserRequest(string email, string newUserName, string newUserRole)
    {
        Email = email;
        NewUserName = newUserName;
        NewUserRole = newUserRole;
    }
}