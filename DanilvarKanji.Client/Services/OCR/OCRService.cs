using DanilvarKanji.Shared.Requests.OCR;

namespace DanilvarKanji.Client.Services.OCR;

public class OCRService : IOCRService
{
    private readonly HttpClient _httpClient;

    public OCRService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ServerApi");
    }

    public async Task<string> ExtractTextAsync(ExtractTextRequest request)
    {
        var formData = new MultipartFormDataContent();
        if (request.Image != null)
            formData.Add(new StreamContent(request.Image.OpenReadStream()), "Image", request.Image.FileName);
        formData.Add(new StringContent(request.LangMode.ToString()), "LangMode");

        var response = await _httpClient.PostAsync("api/OCR/Extract", formData);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> ExtractTextAsync(byte[] text, string fileName, LangMode mode)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new ByteArrayContent(text), "Image", fileName);
        formData.Add(new StringContent(mode.ToString()), "LangMode");

        var response = await _httpClient.PostAsync("api/OCR/Extract", formData);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}