using AutoMapper;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Requests.Auth;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers.Auth;

[AllowAnonymous]
public class UserController : ApiController
{
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper) : base(mediator)
    {
        _mapper = mapper;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest? request)
    {
        var command = _mapper.Map<RegisterUserCommand>(request);

        IdentityResult result = await Mediator.Send(command);

        if (result.Succeeded)
            return Ok(command);

        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);

        return BadRequest(ModelState);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest? request)
    {
        var command = _mapper.Map<LoginUserCommand>(request);

        Result<LoginResponse> result = await Mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    

    [Authorize]
    [HttpPatch("{userId}:ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId)
    {
        var command = new ConfirmEmailCommand(userId);

        IdentityResult result = await Mediator.Send(command);

        if (result.Succeeded)
            return Ok(command);
        
        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);

        return BadRequest(ModelState);
    }

    [HttpGet("IsAuthorized")]
    public async Task<IActionResult> IsAuthorized()
    {
        if (User.Identity.IsAuthenticated)
        {
            return Ok();
        }

        return NotFound();
    }
}