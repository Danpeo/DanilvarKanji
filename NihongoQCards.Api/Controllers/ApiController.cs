using DanilvarKanji.Domain.Primitives;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Route("api/[controller]s")]
public class ApiController : Controller
{
    protected ApiController(IMediator mediator) => Mediator = mediator;

    protected IActionResult HandleFailure(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(CreateProblemDetails("Validation error",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.Errors)),
            _ => BadRequest(CreateProblemDetails("Bad Request",
                StatusCodes.Status400BadRequest,
                result.Error
            ))
        };
    }


    protected IMediator Mediator { get; }

    protected IActionResult BadRequest(Error error)
        => BadRequest(new ApiErrorResponse(new[] { error }));

    protected new IActionResult Ok(object value) => base.Ok(value);

    protected new IActionResult NotFound() => base.NotFound();

    protected Task<AppUser?> GetCurrentUser(UserManager<AppUser> userManager) => 
        userManager.GetUserAsync(User);

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]?
        errors = null)
    {
        return new ProblemDetails
        {
            Title = title,
            Status = status,
            Detail = error.Message,
            Instance = error.Code,
            Extensions =
            {
                { nameof(errors), errors }
            }
        };
    }
}