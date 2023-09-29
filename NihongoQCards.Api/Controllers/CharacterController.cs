using DanilvarKanji.Attributes;
using DanilvarKanji.DTO;
using DanilvarKanji.Extensions;
using DanilvarKanji.Services;
using DanilvarKanji.Services.Characters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Todo("Add Authorize later")]
[Route("api/[controller]")]
[ApiController]
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
    public async Task<IEnumerable<CharacterDto>> GetAllAsync() => 
        await _characterService.GetAllAsync();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        if (!_characterService.Exist(id))
            return NotFound();
        
        CharacterDto character = await _characterService.GetAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(character);
    }

    [HttpPatch("{id}")]
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