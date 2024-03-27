using System.Net;
using System.Net.Http.Json;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Params;
using DanilvarKanji.Shared.Responses.User;

namespace DanilvarKanji.Client.Services.Auth;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ServerApi");
    }

    public async Task<UserResponseBase?> GetUserAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/Users");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return default;

            return await response.Content.ReadFromJsonAsync<UserResponseBase>();
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
    }

    public async Task<LearningSettings?> GetLearningSettingsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/Users/LearningSettings");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return default;

            return await response.Content.ReadFromJsonAsync<LearningSettings>();
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
    }

    public async Task<IEnumerable<AppUser>?> ListUsersAsync(int pageNumber = 0,
        int pageSize = 0)
    {
        string requestUri = $"api/Users/All?PageNumber={pageNumber}&PageSize={pageSize}";
        return await Http.ListAsync<AppUser>(requestUri, _httpClient);
    }
    
    public async Task DeleteUserAsync(string email)
    {
        string requestUri = $"api/Users/{email}";
        await Http.DeleteAsync(requestUri, _httpClient);
    }
    
    public async Task UpdateLearningSettingsAsync(LearningSettings settings)
    {
        HttpResponseMessage response =
            await _httpClient.PutAsJsonAsync("api/users/UpdateUserLearningSettings", settings);

        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
        }
    }
}