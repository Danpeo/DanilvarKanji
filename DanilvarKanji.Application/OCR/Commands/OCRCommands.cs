using DanilvarKanji.Domain.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DanilvarKanji.Application.OCR.Commands;

public record ExtractTextFromImageCommand(IFormFile? ImageFile, string LangMode) : IRequest<Result<string>>;