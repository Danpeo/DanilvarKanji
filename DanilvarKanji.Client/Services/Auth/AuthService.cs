using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.SessionStorage;
using DanilvarKanji.Shared.Enums;
using DanilvarKanji.Shared.Requests.Auth;
using DanilvarKanji.Shared.Responses.Auth;

namespace DanilvarKanji.Client.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly ISessionStorageService _sessionStorageService;
    private const string _baseUrl = "Accounts";
    private const string JwtKey = nameof(JwtKey);
    private const string RefreshKey = nameof(RefreshKey);
    private string _role;
    private string? _jwtCache = string.Empty;
    public event Action<string?>? LoginChange;

    public AuthService(IHttpClientFactory httpFactory, ISessionStorageService sessionStorageService)
    {
        _httpFactory = httpFactory;
        _sessionStorageService = sessionStorageService;
    }

    public async ValueTask<string> GetJwtAsync()
    {
        if (string.IsNullOrEmpty(_jwtCache))
            _jwtCache = await _sessionStorageService.GetItemAsync<string>(JwtKey);

        return _jwtCache;
    }
    
    public async Task<bool> HasRoleAsync(string role)
    {
        _role = GetRole(await GetJwtAsync());

        return _role == role;
    }

    public async Task<bool> HasAnyOfSpecifiedRolesAsync(IEnumerable<string> roles)
    {
        _role = GetRole(await GetJwtAsync());

        return roles.Any(role => role == _role);
    }

    public async Task<bool> HasAnyRoleAsync()
    {
        _role = GetRole(await GetJwtAsync());

        return !string.IsNullOrEmpty(_role);
    }

    public async Task<RegisterUserRequest?> RegisterUserAsync(RegisterUserRequest request)
    {
        try
        {
            HttpResponseMessage response = await _httpFactory
                .CreateClient("ServerApi")
                .PostAsync($"api/{_baseUrl}/Register",
                    JsonContent.Create(request));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RegisterUserRequest>();
            }

            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<LoginResponse?> LoginAsync(LoginUserRequest request)
    {
        HttpResponseMessage response = await _httpFactory
            .CreateClient("ServerApi")
            .PostAsync($"api/{_baseUrl}/Login",
                JsonContent.Create(request));

        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Wrong authorization credentials.");

        var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (content == null)
            throw new InvalidDataException();

        await _sessionStorageService.SetItemAsync(JwtKey, content.JwtToken);
        await _sessionStorageService.SetItemAsync(RefreshKey, content.RefreshToken);

        LoginChange?.Invoke(GetUsername(content.JwtToken));

        await _sessionStorageService.SetItemAsync("Role", GetRole(content.JwtToken));

        return content;
    }

    public async Task<bool> RefreshAsync()
    {
        var request = new RefreshKeyRequest()
        {
            AccessToken = await _sessionStorageService.GetItemAsync<string>(JwtKey),
            RefreshToken = await _sessionStorageService.GetItemAsync<string>(RefreshKey)
        };

        HttpResponseMessage response = await _httpFactory
            .CreateClient("ServerApi")
            .PostAsync($"api/{_baseUrl}/refresh",
                JsonContent.Create(request));

        if (!response.IsSuccessStatusCode)
        {
            await LogoutAsync();

            return false;
        }

        var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (content == null)
            throw new InvalidDataException();

        await _sessionStorageService.SetItemAsync(JwtKey, content.JwtToken);
        await _sessionStorageService.SetItemAsync(RefreshKey, content.RefreshToken);

        _jwtCache = content.JwtToken;

        return true;
    }

    public async Task LogoutAsync()
    {
        HttpResponseMessage response = await _httpFactory
            .CreateClient("ServerApi")
            .DeleteAsync($"api/{_baseUrl}/Revoke");

        await _sessionStorageService.RemoveItemAsync(JwtKey);
        await _sessionStorageService.RemoveItemAsync(RefreshKey);

        _jwtCache = null;

        await Console.Out.WriteLineAsync($"Revoke gave response {response.StatusCode}");

        LoginChange?.Invoke(null);

        await _sessionStorageService.RemoveItemAsync("IsLoggedIn");
    }

    private string GetUsername(string token)
    {
        var jwt = new JwtSecurityToken(token);

        return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
    }

    private string GetRole(string token)
    {
        if (string.IsNullOrEmpty(token))
            return string.Empty;
        
        var jwt = new JwtSecurityToken(token);

        Claim? role = jwt.Claims.FirstOrDefault(c => c.Type == JwtClaim.UserRole.ToString());

        return role != null ? role.Value : string.Empty;
    }
}