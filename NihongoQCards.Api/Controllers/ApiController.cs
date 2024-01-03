using DanilvarKanji.Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Route("api/[controller]s")]
public class ApiController : Controller
{
    protected ApiController(IMediator mediator) => Mediator = mediator;

    protected IMediator Mediator { get; }

    protected IActionResult BadRequest(Error error) 
        => BadRequest(new ApiErrorResponse(new[] { error }));

    protected new IActionResult Ok(object value) => base.Ok(value);

    protected new IActionResult NotFound() => base.NotFound();
}