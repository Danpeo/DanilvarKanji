using DanilvarKanji.Attributes;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Characters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Todo("Add Authorize later")]
[Route("api/[controller]")]
[ApiController]
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
        
        bool result = await _characterLearningService.UpdateCharacterLearningState(characterDto, user);

        return result ? Ok() : NotFound();
    }
}