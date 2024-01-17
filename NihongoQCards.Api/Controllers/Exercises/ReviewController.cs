using DanilvarKanji.Application.Exercises.Commands;
using DanilvarKanji.Application.Reviews.Commands;
using DanilvarKanji.Application.Reviews.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Shared.Requests.Reviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers.Exercises;

[Authorize]
public class ReviewController : ApiController
{
    private readonly UserManager<AppUser> _userManager;
    
    public ReviewController(IMediator mediator, UserManager<AppUser> userManager) : base(mediator)
    {
        _userManager = userManager;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReviewSession), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateReviewSessionRequest request)
    {
        AppUser? user = await _userManager.GetUserAsync(User);
        
        var command = new CreateReviewSessionCommand(request.ExerciseIds, user!);
 
        var result = await Mediator.Send(command);

        if (result.IsFailure)
            return HandleFailure(result);

        return CreatedAtAction("Get", new { id = result.Value }, command);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Character), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(string id)
    {
        AppUser? user = await _userManager.GetUserAsync(User);
        ReviewSession? reviewSession = await Mediator.Send(new GetReviewSessionQuery(id, user!));

        return reviewSession != null ? Ok(reviewSession) : NotFound(Review.NotFound);
    }
}