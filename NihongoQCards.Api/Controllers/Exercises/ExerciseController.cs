using DanilvarKanji.Application.Exercises.Commands;
using DanilvarKanji.Application.Exercises.Queries;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Shared.Requests.Exercises;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers.Exercises;

[Authorize]
public class ExerciseController : ApiController
{
    private readonly UserManager<AppUser> _userManager;

    public ExerciseController(IMediator mediator, UserManager<AppUser> userManager)
        : base(mediator)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateExerciseRequest request)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
            return Unauthorized();

        var command = new CreateExerciseCommand(
            request.CharacterId,
            user,
            request.IsCorrect,
            request.ExerciseType,
            request.ExerciseSubject
        );

        var result = await Mediator.Send(command);

        command.Id = result.Value;
        if (result.IsSuccess)
            return CreatedAtAction("Get", new { id = command.Id }, command);

        return BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
            return Unauthorized();

        Exercise? exercise = await Mediator.Send(new GetExerciseQuery(id, user));

        return exercise is not null ? Ok(exercise) : NotFound("Exercise was not found");
    }

    [HttpGet]
    public async Task<IActionResult> ListAsync([FromQuery] PaginationParams paginationParams)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
            return Unauthorized();

        var exercises = await Mediator.Send(new ListExercisesQuery(paginationParams, user));

        return exercises.Any() ? Ok(exercises) : NoContent();
    }
}