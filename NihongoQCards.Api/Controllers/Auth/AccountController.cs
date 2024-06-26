using AutoMapper;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Shared.Requests.Auth;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers.Auth;

public class AccountController : ApiController
{
    private readonly IMapper _mapper;

    public AccountController(IMediator mediator, IMapper mapper)
        : base(mediator)
    {
        _mapper = mapper;
    }

    [HttpPost("Register")]
    [ProducesResponseType(typeof(IdentityResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IdentityError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest? request)
    {
        var command = _mapper.Map<RegisterUserCommand>(request);

        IdentityResult result = await Mediator.Send(command);

        if (result.Succeeded)
            return Ok(result);

        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);

        return BadRequest(ModelState);
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest request)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        var result = await Mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("Refresh")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshAsync([FromBody] RefreshKeyRequest request)
    {
        var command = new RefreshKeyCommand(request.AccessToken, request.RefreshToken);

        var result = await Mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [Authorize]
    [HttpDelete("Revoke")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RevokeAsync([FromServices] UserManager<AppUser> _userManager)
    {
        var username = HttpContext.User.Identity?.Name;

        if (username is null)
            return Unauthorized();

        AppUser? user = await _userManager.FindByNameAsync(username);

        if (user is null)
            return Unauthorized();

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return Ok();
    }

    [ProducesResponseType(typeof(IdentityResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IdentityError), StatusCodes.Status400BadRequest)]
    [HttpPatch("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmRegistrationRequest request)
    {
        var command = new ConfirmEmailCommand(request.Email, request.ConfirmationCode);

        IdentityResult result = await Mediator.Send(command);

        if (result.Succeeded)
            return Ok(command);

        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);

        return BadRequest(ModelState);
    }

    [ProducesResponseType(typeof(IdentityResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IdentityError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    [Authorize(Roles = UserRole.SuperAdmin)]
    [HttpPatch("ConfirmEmailForced/{userEmail}")]
    public async Task<IActionResult> ConfirmEmailForced(string userEmail)
    {
        var command = new ConfirmEmailForcedCommand(userEmail);

        IdentityResult result = await Mediator.Send(command);

        if (result.Succeeded)
            return Ok(command);

        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);

        return BadRequest(ModelState);
    }
}