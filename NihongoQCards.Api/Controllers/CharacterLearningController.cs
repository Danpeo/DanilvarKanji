using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Characters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class CharacterLearningController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICharacterLearningService _characterLearningService;

    public CharacterLearningController(UserManager<AppUser> userManager,
        ICharacterLearningService characterLearningService)
    {
        _userManager = userManager;
        _characterLearningService = characterLearningService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCharacterForLearning([FromBody] CharacterForLearnDto characterDto)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (!ModelState.IsValid)
            return BadRequest();

        if (user == null)
            return Unauthorized();

        bool result = await _characterLearningService.AddCharacterLearning(characterDto, user);

        return result ? Ok() : NotFound();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] CharacterForLearnDto characterDto)
    {
        bool result = await _characterLearningService.UpdateCharacterLearning(id, characterDto);

        if (!ModelState.IsValid)
            return BadRequest();

        return result ? Ok() : NotFound();
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAllAsync()
    {
        if (User.Identity is { IsAuthenticated: false })
            return Unauthorized();

        IEnumerable<CharacterForLearnDto> characters = await _characterLearningService.GetAllAsync();
        return Ok(characters);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllForUserAsync()
    {
        if (User.Identity is { IsAuthenticated: false })
            return Unauthorized();

        AppUser? user = await _userManager.GetUserAsync(User);

        IEnumerable<CharacterForLearnDto> characters = await _characterLearningService.GetAllForUserAsync(user);

        return Ok(characters);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetForUserAsync(int id)
    {
        if (User.Identity is { IsAuthenticated: false })
            return Unauthorized();

        if (!await _characterLearningService.Exist(id))
            return NotFound();

        AppUser? user = await _userManager.GetUserAsync(User);
        
        CharacterForLearnDto character = await _characterLearningService.GetForUserAsync(id, user);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(character);
    }

    [HttpGet("FromAll/{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        if (User.Identity is { IsAuthenticated: false })
            return Unauthorized();

        if (!await _characterLearningService.Exist(id))
            return NotFound();
        
        CharacterForLearnDto character = await _characterLearningService.GetAsync(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(character);
    }
}