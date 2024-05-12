namespace DanilvarKanji.Shared.Requests.Auth;

public class ConfirmRegistrationRequest
{
  public ConfirmRegistrationRequest()
  {
  }

  public ConfirmRegistrationRequest(string email, string confirmationCode)
  {
    Email = email;
    ConfirmationCode = confirmationCode;
  }

  public string Email { get; set; } = "";

  public string ConfirmationCode { get; set; } = "";
}