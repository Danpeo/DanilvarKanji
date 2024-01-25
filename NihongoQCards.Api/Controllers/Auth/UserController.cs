using DanilvarKanji.Shared.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers.Auth;

public class UserController : ApiController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromServices] UserManager<AppUser> _userManager)
    {
        AppUser? user = await _userManager.GetUserAsync(User);
        
        if (user != null)
            return Ok(user);

        return Unauthorized();
    }
}