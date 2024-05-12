namespace DanilvarKanji.Shared.Requests.Users;

public class UpdateUserRequest
{
  public UpdateUserRequest(string newUserName, string newUserRole)
  {
    NewUserName = newUserName;
    NewUserRole = newUserRole;
  }

  public string NewUserName { get; set; }
  public string NewUserRole { get; set; }
}