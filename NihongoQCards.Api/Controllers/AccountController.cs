using AutoMapper;
using CloudinaryDotNet.Actions;
using DanilvarKanji.Services.Auth;
using DanilvarKanji.Services.Images;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Entities;
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
    private readonly IImageService _imageService;

    public AccountController(IMemberService memberService, IMapper mapper, UserManager<AppUser> userManager,
        ITokenService tokenService, IImageService imageService)
    {
        _memberService = memberService;
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
        _imageService = imageService;
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
            .ThenInclude(x => x.LearningProgress)
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

    [HttpPost("SetProfileImg")]
    public async Task<IActionResult> SetProfileImage(IFormFile file)
    {
        ImageUploadResult? result = await _imageService.UploadImageAsync(file);

        if (result?.Error != null)
            return BadRequest(result.Error.Message);

        AppUser? user = await _userManager.GetUserAsync(User);

        if (result != null)
        {
            var image = new Image()
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user != null)
                user.ProfileImage = image;

            return Ok("Profile Image was successfully uploaded");
        }

        return BadRequest("Problem uploading Profile Image");
    }
}