using AutoMapper;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AccountController(IUserService userService, IMapper mapper, UserManager<AppUser> userManager,
        ITokenService tokenService)
    {
        _userService = userService;
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await _userService.Exist(registerDto.UserName))
            return BadRequest("User Name is taken");

        var user = _mapper.Map<AppUser>(registerDto);
        user.UserName = registerDto.UserName.ToLower();

        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return new UserDto
        {
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user),
            JlptLevel = user.JlptLevel
        };
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        AppUser? user = await _userManager.Users
            .Include(x => x.CharacterLearnings)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

        if (user == null) 
            return Unauthorized("Invalid User Name");
        
        bool result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result) 
            return Unauthorized("Invalid password");

        return new UserDto
        {
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user),
            JlptLevel = user.JlptLevel
        };
    }
}