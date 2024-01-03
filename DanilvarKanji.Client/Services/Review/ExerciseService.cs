using System.Net.Http.Json;
using DanilvarKanji.Shared.Requests.Exercises;
using DanilvarKanji.Shared.Responses.Exercise;

namespace DanilvarKanji.Client.Services.Review;

public class ExerciseService : IExerciseService
{
    private readonly HttpClient _httpClient;

    public ExerciseService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ServerApi");
    }
    
    public async Task<GetAllFromExerciseResponse?> CreateExerciseAsync(CreateExerciseRequest request)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Exercises", request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<GetAllFromExerciseResponse>();

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