using System.Net;
using System.Net.Http.Json;

namespace DanilvarKanji.Client.Services;

public static class Http
{
  public static async Task<TResponse?> PostAsync<TResponse, TRequest>(
    TRequest request,
    string requestUri,
    HttpClient http
  )
  {
    HttpResponseMessage response = await http.PostAsJsonAsync(requestUri, request);

    if (response.IsSuccessStatusCode)
      return await response.Content.ReadFromJsonAsync<TResponse>();

    var message = await response.Content.ReadAsStringAsync();
    throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
  }

  public static async Task PostAsync<TRequest>(TRequest request, string requestUri, HttpClient http)
  {
    HttpResponseMessage response = await http.PostAsJsonAsync(requestUri, request);

    if (!response.IsSuccessStatusCode)
    {
      var message = await response.Content.ReadAsStringAsync();
      throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
    }
  }

  public static async Task PutAsync<TRequest>(TRequest request, string requestUri, HttpClient http)
  {
    HttpResponseMessage response = await http.PutAsJsonAsync(requestUri, request);

    if (!response.IsSuccessStatusCode)
    {
      var message = await response.Content.ReadAsStringAsync();
      throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
    }
  }

  public static async Task<TResponse?> PutAsync<TRequest, TResponse>(
    TRequest request,
    string requestUri,
    HttpClient http
  )
  {
    HttpResponseMessage response = await http.PutAsJsonAsync(requestUri, request);

    if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<TResponse>();

    var message = await response.Content.ReadAsStringAsync();
    throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
  }

  public static async Task PatchAsync<TRequest>(
    TRequest request,
    string requestUri,
    HttpClient http
  )
  {
    HttpResponseMessage response = await http.PatchAsJsonAsync(requestUri, request);

    if (!response.IsSuccessStatusCode)
    {
      var message = await response.Content.ReadAsStringAsync();
      throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
    }
  }

  public static async Task<IEnumerable<TResponse>?> EnumerateAsync<TResponse>(
    string requestUri,
    HttpClient http
  )
  {
    return await ProcessListAsync<IEnumerable<TResponse>>(requestUri, http);
  }

  public static async Task<List<TResponse>?> ListAsync<TResponse>(
    string requestUri,
    HttpClient http
  )
  {
    return await ProcessListAsync<List<TResponse>>(requestUri, http);
  }

  public static async Task<TResponse?> GetAsync<TResponse>(string requestUri, HttpClient http)
  {
    HttpResponseMessage response = await http.GetAsync(requestUri);

    if (response.IsSuccessStatusCode)
    {
      if (response.StatusCode == HttpStatusCode.NoContent)
        return default;

      return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    var message = await response.Content.ReadAsStringAsync();
    throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
  }

  public static async Task DeleteAsync(string requestUri, HttpClient http)
  {
    HttpResponseMessage response = await http.DeleteAsync(requestUri);

    if (response.IsSuccessStatusCode)
      return;

    var message = await response.Content.ReadAsStringAsync();
    throw new HttpRequestException($"Http status:{response.StatusCode} Message -{message}");
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

    var message = await response.Content.ReadAsStringAsync();
    throw new HttpRequestException($"Http status code: {response.StatusCode} message: {message}");
  }
}