using DanilvarKanji.Attributes;
using DanilvarKanji.DTO;
using DanilvarKanji.Services.Characters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CharacterDto characterDto)
    {
        bool result = await _characterService.CreateAsync(characterDto);

        if (!ModelState.IsValid)
            return BadRequest();

        return result ? Ok() : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        if (User.Identity is { IsAuthenticated: false })
            return Unauthorized();

        IEnumerable<CharacterDto> characters = await _characterService.GetAllAsync();
        return Ok(characters);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        if (User.Identity is { IsAuthenticated: false })
            return Unauthorized();

        if (!await _characterService.Exist(id))
            return NotFound();

        CharacterDto character = await _characterService.GetAsync(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(character);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] CharacterDto characterDto)
    {
        bool result = await _characterService.UpdateAsync(id, characterDto);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return result ? Ok() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id) =>
        await _characterService.DeleteAsync(id) ? Ok() : NotFound();
}