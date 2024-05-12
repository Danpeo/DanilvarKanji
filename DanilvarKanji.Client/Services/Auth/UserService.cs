using System.Net;
using System.Net.Http.Json;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Shared.Requests.Users;
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
    HttpResponseMessage response = await _httpClient.GetAsync("api/Users");

    if (response.IsSuccessStatusCode)
    {
      if (response.StatusCode == HttpStatusCode.NoContent)
        return default;

      return await response.Content.ReadFromJsonAsync<UserResponseBase>();
    }

    var message = await response.Content.ReadAsStringAsync();
    throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
  }

  public async Task<LearningSettings?> GetLearningSettingsAsync()
  {
    HttpResponseMessage response = await _httpClient.GetAsync("api/Users/LearningSettings");

    if (response.IsSuccessStatusCode)
    {
      if (response.StatusCode == HttpStatusCode.NoContent)
        return default;

      return await response.Content.ReadFromJsonAsync<LearningSettings>();
    }

    var message = await response.Content.ReadAsStringAsync();
    throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
  }

  public async Task<IEnumerable<AppUser>?> ListUsersAsync(int pageNumber = 0, int pageSize = 0)
  {
    var requestUri = $"api/Users/All?PageNumber={pageNumber}&PageSize={pageSize}";
    return await Http.ListAsync<AppUser>(requestUri, _httpClient);
  }

  public async Task DeleteUserAsync(string email)
  {
    var requestUri = $"api/Users/{email}";
    await Http.DeleteAsync(requestUri, _httpClient);
  }

  public async Task UpdateLearningSettingsAsync(LearningSettings settings)
  {
    HttpResponseMessage response = await _httpClient.PutAsJsonAsync(
      "api/users/UpdateUserLearningSettings",
      settings
    );

    if (!response.IsSuccessStatusCode)
    {
      var message = await response.Content.ReadAsStringAsync();
      throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
    }
  }

  public async Task UpdateUserAsync(string userEmail, UpdateUserRequest request)
  {
    await Http.PutAsync(request, $"api/Users/UpdateUser?email={userEmail}", _httpClient);
  }

  public async Task ConfirmEmailForUserAsync(string userEmail)
  {
    await Http.PatchAsync(userEmail, $"api/Accounts/ConfirmEmailForced/{userEmail}", _httpClient);
  }
}