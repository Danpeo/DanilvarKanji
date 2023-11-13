using AutoMapper;
using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Requests.Characters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace DanilvarKanji.Controllers.Characters;

//[Authorize]
/*[ApiController]
[Route("Api/[controller]s")]*/
[AllowAnonymous]
public class CharacterController : ApiController
{
    private readonly ICharacterRepository _characterRepository;

    private readonly IMapper _mapper;


    public CharacterController(ICharacterRepository characterRepository, IMediator mediator, IMapper mapper) : base(mediator)
    {
        _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }

    /*[HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CharacterDto characterDto)
    {
        if (await _characterRepository.CreateAsyncObsolete(characterDto))
            return CreatedAtAction("Get", new { id = characterDto.Id }, characterDto);

        return BadRequest("Error when creating a characater.");
    }*/

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCharacterRequest? request)
    {
        var command = _mapper.Map<CreateCharacterCommand>(request);
        var character = _mapper.Map<Character>(command);
        
        return await Result.Create(command, General.UnProcessableRequest)
            .Bind(c => Mediator.Send(c))
            .Match(() => CreatedAtAction("Get", new {id = character.Id}, character), BadRequest);
    }


    [EnableQuery, HttpGet]
    public async Task<IActionResult> ListAsync([FromQuery] ListCharactersRequest request)
    {
        var query = _mapper.Map<ListCharactersQuery>(request);

        var characters = await Mediator.Send(query);

        return characters.Any() ? Ok(characters) : NotFound("Not characters found");
    }

    [HttpGet("{searchTerm}:Search")]
    public async Task<IActionResult> SearchAsync([FromQuery] PaginationParams paginationParams,
        string searchTerm = "any")
    {
        if (await _characterRepository.AnyExist())
        {
            IEnumerable<CharacterDto> characters = await _characterRepository.SearchAsync(searchTerm, paginationParams);
            return Ok(characters);
        }

        return NotFound("No characters");
    }

    [EnableQuery, HttpGet("{id}:Child")]
    public async Task<IActionResult> ListChildCharacters(string id)
    {
        if (!await _characterRepository.Exist(id))
            return NotFound("Character with this ID was not found");

        IEnumerable<CharacterDto> characters = await _characterRepository.ListChildCharacters(id);
        return Ok(characters);
    }

    [EnableQuery, HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        if (!await _characterRepository.Exist(id))
            return NotFound("Character with this ID was not found");

        CharacterDto character = await _characterRepository.GetAsync(id);

        return Ok(character);
    }

    [EnableQuery, HttpGet("{id}:KanjiMeanings")]
    public async Task<IActionResult> GetKanjiMeaningsByPriorityAsync(string id, int takeQty, Culture culture)
    {
        if (!await _characterRepository.Exist(id))
            return NotFound("Character with this ID was not found");

        CharacterDto character = await _characterRepository.GetAsync(id);

        IEnumerable<string> kanjiMeanings =
            await _characterRepository.GetKanjiMeaningsByPriority(character.Id, takeQty, culture);

        return Ok(kanjiMeanings);
    }

    [HttpGet("{id}/Partial")]
    public async Task<IActionResult> GetPartialAsync(string id, [FromQuery] IEnumerable<string> fields)
    {
        if (!await _characterRepository.Exist(id))
            return NotFound("Character with this ID was not found");

        CharacterDto? character = await _characterRepository.GetPartialAsync(id, fields);

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
        if (await _characterRepository.UpdateAsync(id, characterDto))
        {
            CharacterDto updatedCharacter = await _characterRepository.GetAsync(id);
            return Ok(updatedCharacter);
        }

        return NotFound("The character was not found or an error occurred during the update.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ReplaceAsync(string id, [FromBody] CharacterDto characterDto)
    {
        if (await _characterRepository.ReplaceAsync(id, characterDto))
        {
            CharacterDto updatedCharacter = await _characterRepository.GetAsync(id);
            return Ok(updatedCharacter);
        }

        return NotFound("The character was not found or an error occurred during the replace.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        if (await _characterRepository.DeleteAsync(id))
            return Ok("The character was successfully deleted.");

        return NotFound("The character was not found or an error occurred during the delete.");
    }
}