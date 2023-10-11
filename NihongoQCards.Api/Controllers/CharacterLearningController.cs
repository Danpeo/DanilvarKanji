using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Characters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]s")]
public class CharacterLearningController : ControllerBase
{
    private readonly ICharacterLearningService _characterLearningService;
    private readonly ICharacterLearningManagementService _charLearnManageService;
    private readonly UserManager<AppUser> _userManager;

    public CharacterLearningController(ICharacterLearningService characterLearningService,
        UserManager<AppUser> userManager, ICharacterLearningManagementService charLearnManageService)
    {
        _characterLearningService = characterLearningService;
        _userManager = userManager;
        _charLearnManageService = charLearnManageService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCharacterForLearning([FromBody] CharacterLearningDto characterDto)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (!ModelState.IsValid)
            return BadRequest();

        if (user == null)
            return Unauthorized();

        bool result = await _charLearnManageService.AddCharacterLearning(characterDto, user);

        return result ? Ok() : NotFound();
    }

    [HttpPatch("{id:Guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CharacterLearningDto characterDto)
    {
        bool result = await _charLearnManageService.UpdateCharacterLearning(id, characterDto);

        if (!ModelState.IsValid)
            return BadRequest();

        return result ? Ok() : NotFound();
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAllAsync()
    {
        IEnumerable<CharacterLearningDto> characters = await _charLearnManageService.GetAllAsync();
        return Ok(characters);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllForUserAsync()
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        IEnumerable<CharacterLearningDto> characters = await _charLearnManageService.GetAllForUserAsync(user);

        return Ok(characters);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetForUserAsync(Guid id)
    {
        if (!await _charLearnManageService.Exist(id))
            return NotFound();

        AppUser? user = await _userManager.GetUserAsync(User);

        CharacterLearningDto character = await _charLearnManageService.GetForUserAsync(id, user);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(character);
    }

    [HttpGet("FromAll/{id:Guid}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        if (!await _charLearnManageService.Exist(id))
            return NotFound();

        CharacterLearningDto character = await _charLearnManageService.GetAsync(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(character);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return await _charLearnManageService.DeleteAsync(id) ? Ok() : NotFound();
    }

    [HttpDelete("FromAll/{id:Guid}")]
    public async Task<IActionResult> DeleteForUserAsync(Guid id)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        return user != null && await _charLearnManageService.DeleteForUserAsync(id, user) ? Ok() : NotFound();
    }

    [HttpPatch("Increase/{id:Guid}")]
    public async Task<IActionResult> IncreaseLearningProgressAsync(Guid id, float value)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        bool result = user != null &&
                      await _characterLearningService.IncreaseLearningRateAsync(id, user, value);

        if (!ModelState.IsValid)
            return BadRequest();

        return result ? Ok() : NotFound();
    }

    [HttpPatch("Decrease/{id:Guid}")]
    public async Task<IActionResult> DecreaseLearningProgressAsync(Guid id, float value)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        bool result = user != null &&
                      await _characterLearningService.DecreaseLearningRateAsync(id, user, value);

        if (!ModelState.IsValid)
            return BadRequest();

        return result ? Ok() : NotFound();
    }
}