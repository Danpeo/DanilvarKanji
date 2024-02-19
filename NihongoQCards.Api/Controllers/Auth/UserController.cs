using DanilvarKanji.Application.Users.Commands;
using DanilvarKanji.Application.Users.Queries;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Params;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers.Auth;

[Authorize]
public class UserController : ApiController
{
    private readonly UserManager<AppUser> _userManager;

    public UserController(IMediator mediator, UserManager<AppUser> userManager) : base(mediator)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        AppUser? user = await GetCurrentUser(_userManager);

        if (user != null)
            return Ok(user);

        return Unauthorized();
    }

    [HttpGet("LearningSettings")]
    [ProducesResponseType(typeof(LearningSettings), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetUserLearningSettingsAsync()
    {
        AppUser? user = await GetCurrentUser(_userManager);

        LearningSettings? settings = await Mediator.Send(new GetUserLearningSettingsQuery(user!.Email!));
        
        if (settings is not null)
            return Ok(settings);

        return NoContent();
    }

    [HttpPut("UpdateUserLearningSettings")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUserLearningSettingsAsync([FromBody] LearningSettings learningSettings)
    {
        AppUser? user = await GetCurrentUser(_userManager);

        var result = await Mediator.Send(new UpdateUserLearningSettingsCommand(user!.Email!, learningSettings));
        
        if (result.IsSuccess)
            return Ok(result.Value);

        return HandleFailure(result);
    }

    [HttpPut("UpdateUserXpAsync")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUserXpAsync([FromBody] int xp)
    {
        AppUser? user = await GetCurrentUser(_userManager);

        var result = await Mediator.Send(new UpdateUserXpCommand(xp, user!.Email!));
        
        if (result.IsSuccess)
            return Ok(result.Value);

        return HandleFailure(result);
    }
}