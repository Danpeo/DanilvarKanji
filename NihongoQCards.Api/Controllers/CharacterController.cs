using DanilvarKanji.DTO;
using DanilvarKanji.Services.Characters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

//[Authorize]
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

        if (result)
            return CreatedAtAction("Get", new { id = characterDto.Id }, characterDto);

        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        IEnumerable<CharacterDto> characters = await _characterService.ListAsync();
        return Ok(characters);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        if (!await _characterService.Exist(id))
            return NotFound();

        CharacterDto character = await _characterService.GetAsync(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(character);
    }

    [HttpGet("{id}/partial")]
    public async Task<IActionResult> GetPartialAsync(string id, [FromQuery] List<string> fields)
    {
        if (!await _characterService.Exist(id))
        {
            return NotFound();
        }

        CharacterDto? character = await _characterService.GetPartialAsync(id, fields);

        if (character == null)
            return NotFound();

        Dictionary<string,object?> nonNullFields = character.GetType()
            .GetProperties()
            .Where(prop => prop.GetValue(character) != null)
            .ToDictionary(prop => prop.Name, prop => prop.GetValue(character));

        return Ok(nonNullFields);
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] CharacterDto characterDto)
    {
        bool result = await _characterService.UpdateAsync(id, characterDto);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (result)
        {
            CharacterDto updatedCharacter = await _characterService.GetAsync(id);
            return Ok(updatedCharacter);
        }

        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ReplaceAsync(string id, [FromBody] CharacterDto characterDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool result = await _characterService.ReplaceAsync(id, characterDto);

        if (result)
        {
            CharacterDto updatedCharacter = await _characterService.GetAsync(id);
            return Ok(updatedCharacter);
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        return await _characterService.DeleteAsync(id) ? Ok() : NotFound();
    }
}