using DanilvarKanji.Services.Characters;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.RepositoryAbstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace DanilvarKanji.Controllers.Characters;

[ApiController]
[Route("Api/[controller]s")]
public class KanjiMeaningController : ControllerBase
{
    private readonly ICharacterRepository _characterRepository;

    public KanjiMeaningController(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }
    
    [EnableQuery]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        if (!await _characterRepository.Exist(id))
            return NotFound("Character with this ID was not found");

        CharacterDto character = await _characterRepository.GetAsyncObsolete(id);
        
        return Ok(character);
    }
}