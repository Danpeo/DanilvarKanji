using Microsoft.AspNetCore.Http;

namespace DanilvarKanji.Shared.Requests.OCR;

public record ExtractTextRequest(IFormFile? Image, LangMode LangMode);