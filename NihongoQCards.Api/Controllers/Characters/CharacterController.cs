using DanilvarKanji.Services.Characters;
using DanilvarKanji.Shared.DTO;
using DanilvarKanji.Shared.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DanilvarKanji.Controllers.Characters;

//[Authorize]
[ApiController]
[Route("Api/[controller]s")]
public class CharacterController : ODataController
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CharacterDto characterDto)
    {
        if (await _characterService.CreateAsync(characterDto))
            return CreatedAtAction("Get", new { id = characterDto.Id }, characterDto);

        return BadRequest("Error when creating a characater.");
    }


    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        if (await _characterService.AnyExist())
        {
            IEnumerable<CharacterDto> characters = await _characterService.ListAsync();
            return Ok(characters);
        }

        return NotFound("No characters");
    }

    [EnableQuery]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        if (!await _characterService.Exist(id))
            return NotFound("Character with this ID was not found");

        CharacterDto character = await _characterService.GetAsync(id);

        return Ok(character);
    }

    [EnableQuery]
    [HttpGet("{id}/KanjiMeanings")]
    public async Task<IActionResult> GetKanjiMeaningsByPriorityAsync(string id, int takeQty, Culture culture)
    {
        if (!await _characterService.Exist(id))
            return NotFound("Character with this ID was not found");

        CharacterDto character = await _characterService.GetAsync(id);

        IEnumerable<string> kanjiMeanings = await _characterService.GetKanjiMeaningsByPriority(character.Id, takeQty, culture);

        return Ok(kanjiMeanings);
    }

    [HttpGet("{id}/Partial")]
    public async Task<IActionResult> GetPartialAsync(string id, [FromQuery] IEnumerable<string> fields)
    {
        if (!await _characterService.Exist(id))
            return NotFound("Character with this ID was not found");

        CharacterDto? character = await _characterService.GetPartialAsync(id, fields);

        if (character != null)
        {
            var nonNullFields = character.GetType()
                .GetProperties()
                .Where(prop => prop.GetValue(character) != null)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(character));

            return Ok(nonNullFields);
        }

        return NotFound("Partial data was not found");
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] CharacterDto characterDto)
    {
        if (await _characterService.UpdateAsync(id, characterDto))
        {
            CharacterDto updatedCharacter = await _characterService.GetAsync(id);
            return Ok(updatedCharacter);
        }

        return NotFound("The character was not found or an error occurred during the update.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ReplaceAsync(string id, [FromBody] CharacterDto characterDto)
    {
        if (await _characterService.ReplaceAsync(id, characterDto))
        {
            CharacterDto updatedCharacter = await _characterService.GetAsync(id);
            return Ok(updatedCharacter);
        }

        return NotFound("The character was not found or an error occurred during the replace.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        if (await _characterService.DeleteAsync(id))
            return Ok("The character was successfully deleted.");

        return NotFound("The character was not found or an error occurred during the delete.");
    }
}