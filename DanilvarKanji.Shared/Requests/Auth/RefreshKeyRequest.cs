namespace DanilvarKanji.Shared.Requests.Auth;

public class RefreshKeyRequest
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}