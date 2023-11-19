namespace DanilvarKanji.Shared.Responses.Auth;

public class TokenResponse
{
    public string Token { get; }

    public TokenResponse(string token)
    {
        Token = token;
    }
}