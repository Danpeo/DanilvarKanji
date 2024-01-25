using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.LocalStorage;
using DanilvarKanji.Shared.Enums;
using DanilvarKanji.Shared.Requests.Auth;
using DanilvarKanji.Shared.Responses.Auth;

namespace DanilvarKanji.Client.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly ILocalStorageService _localStorageService;
    private const string _baseUrl = "Accounts";
    private const string JwtKey = nameof(JwtKey);
    private const string RefreshKey = nameof(RefreshKey);
    private string _role = string.Empty;
    private string? _jwtCache = string.Empty;
    public event Action<string?>? LoginChange;

    public AuthService(IHttpClientFactory httpFactory, ILocalStorageService localStorageService)
    {
        _httpFactory = httpFactory;
        _localStorageService = localStorageService;
    }

    public async Task<string> GetJwtAsync()
    {
        if (string.IsNullOrEmpty(_jwtCache))
            _jwtCache = await _localStorageService.GetItemAsync<string>(JwtKey);

        return _jwtCache;
    }

    public async ValueTask<bool> HasRoleAsync(string role)
    {
        _role = GetRole(await GetJwtAsync());

        return _role == role;
    }

    public async ValueTask<bool> HasAnyOfSpecifiedRolesAsync(IEnumerable<string> roles)
    {
        _role = GetRole(await GetJwtAsync());

        return roles.Any(role => role == _role);
    }

    public async ValueTask<bool> HasAnyRoleAsync()
    {
        try
        {
            _role = GetRole(await GetJwtAsync());

            return !string.IsNullOrEmpty(_role);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
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

        await _localStorageService.SetItemAsync(JwtKey, content.JwtToken);
        await _localStorageService.SetItemAsync(RefreshKey, content.RefreshToken);

        LoginChange?.Invoke(GetUsername(content.JwtToken));

        await _localStorageService.SetItemAsync("Role", GetRole(content.JwtToken));

        return content;
    }

    public async Task<bool> RefreshAsync()
    {
        var request = new RefreshKeyRequest()
        {
            AccessToken = await _localStorageService.GetItemAsync<string>(JwtKey),
            RefreshToken = await _localStorageService.GetItemAsync<string>(RefreshKey)
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

        await _localStorageService.SetItemAsync(JwtKey, content.JwtToken);
        await _localStorageService.SetItemAsync(RefreshKey, content.RefreshToken);

        _jwtCache = content.JwtToken;

        return true;
    }

    public async Task LogoutAsync()
    {
        HttpResponseMessage response = await _httpFactory
            .CreateClient("ServerApi")
            .DeleteAsync($"api/{_baseUrl}/Revoke");

        await _localStorageService.RemoveItemAsync(JwtKey);
        await _localStorageService.RemoveItemAsync(RefreshKey);

        _jwtCache = null;

        await Console.Out.WriteLineAsync($"Revoke gave response {response.StatusCode}");

        LoginChange?.Invoke(null);

        await _localStorageService.RemoveItemAsync("IsLoggedIn");
    }

    private string GetUsername(string token)
    {
        var jwt = new JwtSecurityToken(token);

        return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
    }

    private static string GetRole(string token)
    {
        if (string.IsNullOrEmpty(token))
            return string.Empty;

        var jwt = new JwtSecurityToken(token);

        Claim? role = jwt.Claims.FirstOrDefault(c => c.Type == JwtClaim.UserRole.ToString());

        return role != null ? role.Value : string.Empty;
    }
}