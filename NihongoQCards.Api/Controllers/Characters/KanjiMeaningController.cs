using DanilvarKanji.Services.Characters;
using DanilvarKanji.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace DanilvarKanji.Controllers.Characters;

[ApiController]
[Route("Api/[controller]s")]
public class KanjiMeaningController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public KanjiMeaningController(ICharacterService characterService)
    {
        _characterService = characterService;
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
}