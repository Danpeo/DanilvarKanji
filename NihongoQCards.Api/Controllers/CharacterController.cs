using DanilvarKanji.DTO;
using DanilvarKanji.Services.Characters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]s")]
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
        IEnumerable<CharacterDto> characters = await _characterService.GetAllAsync();
        return Ok(characters);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        if (!await _characterService.Exist(id))
            return NotFound();

        CharacterDto character = await _characterService.GetAsync(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(character);
    }

    [HttpPatch("{id:Guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CharacterDto characterDto)
    {
        bool result = await _characterService.UpdateAsync(id, characterDto);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return result ? Ok() : NotFound();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return await _characterService.DeleteAsync(id) ? Ok() : NotFound();
    }
}