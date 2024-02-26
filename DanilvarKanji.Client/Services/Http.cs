using System.Net;
using System.Net.Http.Json;
using DanilvarKanji.Shared.Responses.Review;

namespace DanilvarKanji.Client.Services;

public static class Http
{
    public static async Task<TResponse?> PostAsync<TResponse, TRequest>(TRequest request, string requestUri,
        HttpClient http)
    {
        HttpResponseMessage response =
            await http.PostAsJsonAsync(requestUri, request);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<TResponse>();

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
    }

    public static async Task PostAsync<TRequest>(TRequest request, string requestUri, HttpClient http)
    {
        HttpResponseMessage response =
            await http.PostAsJsonAsync(requestUri, request);

        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
        }
    }

    public static async Task PutAsync<TRequest>(TRequest request, string requestUri, HttpClient http)
    {
        HttpResponseMessage response =
            await http.PutAsJsonAsync(requestUri, request);

        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
        }
    }

    public static async Task<IEnumerable<TResponse>?> EnumerateAsync<TResponse>(string requestUri, HttpClient http) =>
        await ProcessListAsync<IEnumerable<TResponse>>(requestUri, http);

    public static async Task<List<TResponse>?> ListAsync<TResponse>(string requestUri, HttpClient http) =>
        await ProcessListAsync<List<TResponse>>(requestUri, http);

    public static async Task<TResponse?> GetAsync<TResponse>(string requestUri, HttpClient http)
    {
        HttpResponseMessage response = await http.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return default;

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
    }

    private static async Task<T?> ProcessListAsync<T>(string requestUri, HttpClient http)
    {
        HttpResponseMessage response = await http.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return default;

            return await response.Content.ReadFromJsonAsync<T>() ?? default(T);
        }

        string message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
    }
}