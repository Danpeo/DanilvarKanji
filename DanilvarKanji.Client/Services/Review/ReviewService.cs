using System.Net;
using System.Net.Http.Json;
using DanilvarKanji.Shared.Requests.Reviews;
using DanilvarKanji.Shared.Responses.Review;

namespace DanilvarKanji.Client.Services.Review;

public class ReviewService : IReviewService
{
    private readonly HttpClient _httpClient;

    public ReviewService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ServerApi");
    }

    public async Task<ReviewSessionResponseBase?> CreateReviewSessionAsync(
        CreateReviewSessionRequest request
    )
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            "api/ReviewSessions",
            request
        );

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<ReviewSessionResponseBase>();

        var message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
    }

    public async Task<ReviewSessionResponseBase?> GetReviewSessionAsync(string? id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/ReviewSessions/{id}");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return default;

            return await response.Content.ReadFromJsonAsync<ReviewSessionResponseBase>();
        }

        var message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
    }
}