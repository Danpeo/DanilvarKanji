using DanilvarKanji.Domain.Primitives;
using DanilvarKanji.Domain.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DanilvarKanji.Controllers;

[Authorize]
[Route("api/[controller]s")]
public class ApiController : ODataController
{
    protected ApiController(IMediator mediator) => Mediator = mediator;

    protected IMediator Mediator { get; }

    protected IActionResult BadRequest(Error error) 
        => BadRequest(new ApiErrorResponse(new[] { error }));

    protected new IActionResult Ok(object value) => base.Ok(value);

    protected new IActionResult NotFound() => base.NotFound();
}