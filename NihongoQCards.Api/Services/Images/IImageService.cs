using CloudinaryDotNet.Actions;

namespace DanilvarKanji.Services.Images;

public interface IImageService
{
    Task<ImageUploadResult?> UploadImageAsync(IFormFile? file, int height = 500, int width = 500);
    Task<DeletionResult> DeleteImageAsync(string url);
}