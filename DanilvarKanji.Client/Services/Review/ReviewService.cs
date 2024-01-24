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

    public async Task<GetBaseReviewSessionResponse?> CreateReviewSessionAsync(CreateReviewSessionRequest request)
    {
        try
        {
            HttpResponseMessage response =
                await _httpClient.PostAsJsonAsync($"api/ReviewSessions", request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<GetBaseReviewSessionResponse>();

            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<GetBaseReviewSessionResponse?> GetReviewSessionAsync(string? id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/ReviewSessions/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default;

                return await response.Content.ReadFromJsonAsync<GetBaseReviewSessionResponse>();
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
}