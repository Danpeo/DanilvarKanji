using DanilvarKanji.Domain.Primitives;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Route("api/[controller]s")]
public class ApiController : Controller
{
    protected ApiController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected IMediator Mediator { get; }

    protected IActionResult HandleFailure(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult
                => BadRequest(
                    CreateProblemDetails(
                        "Validation error",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors
                    )
                ),
            _
                => BadRequest(
                    CreateProblemDetails("Bad Request", StatusCodes.Status400BadRequest, result.Error)
                )
        };
    }

    protected IActionResult BadRequest(Error error)
    {
        return BadRequest(new ApiErrorResponse(new[] { error }));
    }

    protected new IActionResult Ok(object value)
    {
        return base.Ok(value);
    }

    protected new IActionResult NotFound()
    {
        return base.NotFound();
    }

    protected Task<AppUser?> GetCurrentUser(UserManager<AppUser> userManager)
    {
        return userManager.GetUserAsync(User);
    }

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null
    )
    {
        return new ProblemDetails
        {
            Title = title,
            Status = status,
            Detail = error.Message,
            Instance = error.Code,
            Extensions = { { nameof(errors), errors } }
        };
    }
}