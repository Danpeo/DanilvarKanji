using DanilvarKanji.Application.OCR.Commands;
using DanilvarKanji.Domain.Primitives;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Settings;
using DVar.TextFromImage;
using MediatR;
using Microsoft.Extensions.Options;

namespace DanilvarKanji.Application.OCR.Handlers;

public class ExtractTextFromImageHandler
    : IRequestHandler<ExtractTextFromImageCommand, Result<string>>
{
    private readonly TesseractSettings _settings;

    public ExtractTextFromImageHandler(IOptions<TesseractSettings> options)
    {
        _settings = options.Value;
    }

    public async Task<Result<string>> Handle(
        ExtractTextFromImageCommand request,
        CancellationToken cancellationToken
    )
    {
        using var ms = new MemoryStream();
        if (request.ImageFile == null)
            return Result.Failure<string>(new Error("File.NotProvided", "File is not provided."));

        await request.ImageFile.CopyToAsync(ms, cancellationToken);
        try
        {
            var text = Extractor.ExtractTextFromMemoryStream(
                ms,
                _settings.TessdataPath,
                request.LangMode
            );
            return Result.Success(text);
        }
        catch (Exception e)
        {
            return Result.Failure<string>(new Error("", e.Message));
        }
    }
}