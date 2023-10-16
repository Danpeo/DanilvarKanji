using AutoMapper;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMemberService _memberService;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AccountController(IMemberService memberService, IMapper mapper, UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _memberService = memberService;
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await _memberService.Exist(registerDto.UserName))
        {
            ModelState.AddModelError("Email", "Email is already taken");
            return BadRequest(ModelState);
        }

        var user = _mapper.Map<AppUser>(registerDto);
        user.Email = registerDto.Email.ToLower();
        user.UserName = registerDto.UserName.ToLower();

        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        var userDto = new UserDto
        {
            Email = user.Email,
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user),
            JlptLevel = user.JlptLevel
        };

        return userDto;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        AppUser? user = await _userManager.Users
            .Include(x => x.CharacterLearnings)
            .SingleOrDefaultAsync(x => x.Email == loginDto.Email);

        if (user == null)
        {
            ModelState.AddModelError("Email", "Invalid Email");
            return BadRequest(ModelState);
        }

        bool result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
        {
            ModelState.AddModelError("Password", "Invalid password");
            return BadRequest(ModelState);
        }

        var userDto = new UserDto
        {
            Email = user.Email,
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user),
            JlptLevel = user.JlptLevel
        };

        return userDto;
    }
}
