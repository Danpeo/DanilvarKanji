using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Shared.Requests.Users;

public class UpdateUserRequest
{
    public string NewUserName { get; set; }
    public string NewUserRole { get; set; }

    public UpdateUserRequest(string newUserName, string newUserRole)
    {
        NewUserName = newUserName;
        NewUserRole = newUserRole;
    }
}