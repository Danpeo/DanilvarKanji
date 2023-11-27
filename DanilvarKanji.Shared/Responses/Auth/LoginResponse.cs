namespace DanilvarKanji.Shared.Responses.Auth;

public class LoginResponse
{
    public string JwtToken { get; }
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; }

    public LoginResponse(string jwtToken, string refreshToken, DateTime expiration)
    {
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
        Expiration = expiration;
    }
}