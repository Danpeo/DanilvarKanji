namespace DanilvarKanji.Shared.Requests.Auth;

public class ConfirmRegistrationRequest
{
    public string Email { get; set; } = "";

    public string ConfirmationCode { get; set; } = "";

    public ConfirmRegistrationRequest()
    {
        
    }
    
    public ConfirmRegistrationRequest(string email, string confirmationCode)
    {
        Email = email;
        ConfirmationCode = confirmationCode;
    }
}