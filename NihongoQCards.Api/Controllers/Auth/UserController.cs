using AutoMapper;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Shared.Requests.Auth;
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

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest? request)
    {
        var command = _mapper.Map<RegisterUserCommand>(request);

        IdentityResult result = await Mediator.Send(command);

        if (result.Succeeded)
            return Ok(command);
        
        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);

    }

}