using DanilvarKanji.Services.Characters;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace DanilvarKanji.Controllers.Characters;

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
    public async Task<IActionResult> CreateAsync([FromBody] CharacterLearningDto characterDto)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (await _charLearnManageService.CreateAsync(characterDto, user))
            /*
            return CreatedAtAction("GetForUser", new { id = characterDto.Id }, characterDto);
            */
            return Ok();
        return BadRequest("Error when creating a characater learning.");
    }

    [EnableQuery]
    [HttpGet("All")]
    public async Task<IActionResult> ListAsync()
    {
        if (await _charLearnManageService.AnyExist())
        {
            IEnumerable<CharacterLearningDto> characters = await _charLearnManageService.ListAsync();
            return Ok(characters);
        }

        return NotFound("No character learning");
    }
    
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> ListForUserAsync()
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (await _charLearnManageService.AnyExist(user))
        {
            IEnumerable<CharacterLearningDto> characters = await _charLearnManageService.ListForUserAsync(user);
            return Ok(characters);
        }

        return NotFound("No character learning");
    }

    [EnableQuery]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetForUserAsync(string id)
    {
        if (!await _charLearnManageService.Exist(id))
            return NotFound("Character learning with this ID was not found");

        AppUser? user = await _userManager.GetUserAsync(User);
        CharacterLearningDto character = await _charLearnManageService.GetForUserAsync(id, user);

        return Ok(character);
    }

    [EnableQuery]
    [HttpGet("FromAll/{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        if (!await _charLearnManageService.Exist(id))
            return NotFound("Character learning with this ID was not found");

        CharacterLearningDto character = await _charLearnManageService.GetAsync(id);

        return Ok(character);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] CharacterLearningDto characterDto)
    {
        if (await _charLearnManageService.UpdateAsync(id, characterDto))
        {
            CharacterLearningDto characterLearningDto = await _charLearnManageService.GetAsync(id);
            return Ok(characterLearningDto);
        }

        return NotFound("The character learning was not found or an error occurred during the update.");
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> ReplaceAsync(string id, [FromBody] CharacterLearningDto characterDto)
    {
        if (await _charLearnManageService.ReplaceAsync(id, characterDto))
        {
            CharacterLearningDto updatedCharacter = await _charLearnManageService.GetAsync(id);
            return Ok(updatedCharacter);
        }

        return NotFound("The character was not found or an error occurred during the replace.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        if (await _charLearnManageService.DeleteAsync(id))
            return Ok("The character learning was successfully deleted.");

        return NotFound("The character learning was not found or an error occurred during the delete.");
    }

    [HttpDelete("FromAll/{id}")]
    public async Task<IActionResult> DeleteForUserAsync(string id)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (user != null && await _charLearnManageService.DeleteForUserAsync(id, user))
            return Ok("The character learning was successfully deleted.");

        return NotFound("The character learning was not found or an error occurred during the delete.");
    }

    [HttpPatch("Increase/{id}")]
    public async Task<IActionResult> IncreaseLearningProgressAsync(string id, float value)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        bool result = user != null &&
                      await _characterLearningService.IncreaseLearningRateAsync(id, user, value);

        return result ? Ok() : NotFound();
    }

    [HttpPatch("Decrease/{id}")]
    public async Task<IActionResult> DecreaseLearningProgressAsync(string id, float value)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        bool result = user != null &&
                      await _characterLearningService.DecreaseLearningRateAsync(id, user, value);

        return result ? Ok() : NotFound();
    }
}