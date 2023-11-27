using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.SessionStorage;
using DanilvarKanji.Shared.Requests.Auth;
using DanilvarKanji.Shared.Responses.Auth;

namespace DanilvarKanji.Client.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly ISessionStorageService _sessionStorageService;
    private const string JwtKey = nameof(JwtKey);
    private const string RefreshKey = nameof(RefreshKey);
    private bool _isLoggedIn;
    private string? _jwtCache;
    public bool IsLoggedIn { get; private set; }
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

    public async ValueTask<bool> IsAuthorized()
    {
        _isLoggedIn = await _sessionStorageService.GetItemAsync<bool>("IsLoggedIn");

        return _isLoggedIn;
    }
    
    public async Task<RegisterUserRequest?> RegisterUserAsync(RegisterUserRequest request)
    {
        try
        {
            HttpResponseMessage response = await _httpFactory
                .CreateClient("ServerApi")
                .PostAsync("api/Users/Register",
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
    
    public async Task<LoginResponse> LoginAsync(LoginUserRequest request)
    {
        HttpResponseMessage response = await _httpFactory
            .CreateClient("ServerApi")
            .PostAsync("api/Users/Login",
            JsonContent.Create(request));

        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Login failed.");

        var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (content == null)
            throw new InvalidDataException();

        await _sessionStorageService.SetItemAsync(JwtKey, content.JwtToken);
        await _sessionStorageService.SetItemAsync(RefreshKey, content.RefreshToken);
        await _sessionStorageService.SetItemAsync("IsLoggedIn", true);
        
        LoginChange?.Invoke(GetUsername(content.JwtToken));

        IsLoggedIn = true;
        
        return content;
    }
    
    private static string GetUsername(string token)
    {
        var jwt = new JwtSecurityToken(token);

        return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
    }
    
}