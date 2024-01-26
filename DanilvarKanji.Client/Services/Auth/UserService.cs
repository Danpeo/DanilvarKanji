using System.Net;
using System.Net.Http.Json;
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

    public async Task<GetUserResponse?> GetUserAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Users");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                return await response.Content.ReadFromJsonAsync<GetUserResponse>();
            }

            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<LearningSettings?> GetLearningSettingsAsync()
    {
        try
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
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateLearningSettingsAsync(LearningSettings settings)
    {
        try
        {
            HttpResponseMessage response =
                await _httpClient.PutAsJsonAsync("api/users/UpdateUserLearningSettings", settings);
            
            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}