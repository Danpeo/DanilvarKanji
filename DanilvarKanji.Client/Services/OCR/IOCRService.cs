using DanilvarKanji.Shared.Requests.OCR;

namespace DanilvarKanji.Client.Services.OCR;

public interface IOCRService
{
  Task<string> ExtractTextAsync(ExtractTextRequest request);
  Task<string> ExtractTextAsync(byte[] text, string fileName, LangMode mode);
}