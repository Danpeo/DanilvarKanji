using DanilvarKanji.Application.OCR.Commands;
using DanilvarKanji.Shared.Requests.OCR;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Route("api/[controller]")]
public class OCRController : ApiController
{
  public OCRController(IMediator mediator)
    : base(mediator)
  {
  }

  [HttpPost("Extract")]
  public async Task<IActionResult> ExtractText([FromForm] ExtractTextRequest request)
  {
    var result = await Mediator.Send(
      new ExtractTextFromImageCommand(request.Image, Lang.GetLangModeStr(request.LangMode))
    );

    if (result.IsSuccess)
      return Ok(result.Value);

    return HandleFailure(result);
  }
}