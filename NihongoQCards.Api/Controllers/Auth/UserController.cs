using DanilvarKanji.Application.Users.Commands;
using DanilvarKanji.Application.Users.Queries;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Shared.Requests.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers.Auth;

[Authorize]
public class UserController : ApiController
{
    private readonly UserManager<AppUser> _userManager;

    public UserController(IMediator mediator, UserManager<AppUser> userManager)
        : base(mediator)
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

    /*
    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.SuperAdmin}")]
    */
    [HttpGet("All")]
    [ProducesResponseType(typeof(IEnumerable<AppUser>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListAsync([FromQuery] PaginationParams paginationParams)
    {
        var users = await Mediator.Send(new ListUsersQuery(paginationParams));
        return users.Any() ? Ok(users) : NoContent();
    }

    /*
    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.SuperAdmin}")]
    */
    [HttpDelete("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(string email)
    {
        var result = await Mediator.Send(new DeleteUserCommand(email));
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpGet("LearningSettings")]
    [ProducesResponseType(typeof(LearningSettings), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetUserLearningSettingsAsync()
    {
        AppUser? user = await GetCurrentUser(_userManager);

        LearningSettings? settings = await Mediator.Send(
            new GetUserLearningSettingsQuery(user!.Email!)
        );

        if (settings is not null)
            return Ok(settings);

        return NoContent();
    }

    [HttpPut("UpdateUserLearningSettings")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUserLearningSettingsAsync(
        [FromBody] LearningSettings learningSettings
    )
    {
        AppUser? user = await GetCurrentUser(_userManager);

        var result = await Mediator.Send(
            new UpdateUserLearningSettingsCommand(user!.Email!, learningSettings)
        );

        if (result.IsSuccess)
            return Ok(result.Value);

        return HandleFailure(result);
    }
    
    [HttpPut("UpdateUser")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUserAsync(
        string email,
        [FromBody] UpdateUserRequest request
    )
    {
        var result = await Mediator.Send(
            new UpdateUserCommand(email, request.NewUserName, request.NewUserRole)
        );

        if (result.IsSuccess)
            return Ok(result.Value);

        return HandleFailure(result);
    }
}