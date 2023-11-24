using System.Net.Http.Json;
using DanilvarKanji.Shared.Requests.Auth;

namespace DanilvarKanji.Client.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<RegisterUserRequest?> RegisterUserAsync(RegisterUserRequest user)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Users/Register", user);

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
}