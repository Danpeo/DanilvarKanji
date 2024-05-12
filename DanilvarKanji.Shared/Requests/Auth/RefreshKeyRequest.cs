namespace DanilvarKanji.Shared.Requests.Auth;

public record RefreshKeyRequest(string AccessToken, string RefreshToken);