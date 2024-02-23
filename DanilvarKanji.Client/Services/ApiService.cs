using System.Net.Http.Json;

namespace DanilvarKanji.Client.Services;

public abstract class ApiService
{
    protected async Task<TResponse?> PostAsync<TResponse, TRequest>(TRequest request, string requestUri,
        HttpClient http)
    {
        HttpResponseMessage response =
            await http.PostAsJsonAsync(requestUri, request);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<TResponse>();

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
    }

    protected async Task PostAsync<TRequest>(TRequest request, string requestUri, HttpClient http)
    {
        HttpResponseMessage response =
            await http.PostAsJsonAsync(requestUri, request);

        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
        }
    }
}