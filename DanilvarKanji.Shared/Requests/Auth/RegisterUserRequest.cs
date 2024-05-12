using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Shared.Requests.Auth;

public class RegisterUserRequest
{
  public string UserName { get; set; }

  public JlptLevel JlptLevel { get; set; }

  public string Email { get; set; }

  public string Password { get; set; }

  public string PasswordRepeat { get; set; }
}