using AutoMapper;
using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Requests.Characters;
using DanilvarKanji.Shared.Responses.Character;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers.Characters;

public class CharacterController : ApiController
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public CharacterController(IMediator mediator, IMapper mapper, UserManager<AppUser> userManager) :
        base(mediator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCharacterRequest? request)
    {
        var command = _mapper.Map<CreateCharacterCommand>(request);
        var character = _mapper.Map<Character>(command);

        return await Result.Create(command, General.UnProcessableRequest)
            .Bind(c => Mediator.Send(c))
            .Match(() => CreatedAtAction("Get", new { id = character.Id }, character), BadRequest);
    }

    [HttpGet]
    public async Task<IActionResult> ListAsync([FromQuery] PaginationParams paginationParams)
    {
        IEnumerable<Character> characters = await Mediator.Send(new ListCharactersQuery(paginationParams));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [Authorize]
    [HttpGet("LearnQueue")]
    public async Task<IActionResult> ListLearnQueueAsync([FromQuery] PaginationParams paginationParams)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
            return Unauthorized();

        IEnumerable<GetCharacterBaseInfoResponse> characters =
            await Mediator.Send(new ListLearnQueueQuery(paginationParams, user.JlptLevel, user));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [Authorize]
    [HttpGet("GetNextInLearnQueue")]
    public async Task<IActionResult> GetNextInLearnQueueAsync()
    {
        AppUser? user = await _userManager.GetUserAsync(User);
        if (user is null)
            return Unauthorized();

        GetCharacterBaseInfoResponse? character = await Mediator.Send(new GetNextInLearnQueueQuery(user));

        if (character is not null)
            return Ok(character);

        return NotFound("Character was not fount");
    }

    [HttpGet("{searchTerm}:Search")]
    public async Task<IActionResult> SearchAsync([FromQuery] PaginationParams paginationParams,
        string searchTerm = "any")
    {
        IEnumerable<Character> characters =
            await Mediator.Send(new SearchCharactersQuery(searchTerm, paginationParams));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [HttpGet("{id}:Child")]
    public async Task<IActionResult> ListChildCharacters(string id)
    {
        IEnumerable<Character> characters = await Mediator.Send(new ListChildCharactersQuery(id));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        Character? character = await Mediator.Send(new GetCharacterQuery(id));

        if (character is not null)
            return Ok(character);

        return NotFound("Character with this ID was not found");
    }

    [HttpGet("{id}:KanjiMeanings")]
    public async Task<IActionResult> GetKanjiMeaningsByPriorityAsync(string id, Culture culture = Culture.EnUS,
        int takeQty = int.MaxValue)
    {
        IEnumerable<string> kanjiMeanings =
            await Mediator.Send(new GetKanjiMeaningsByPriorityQuery(id, culture, takeQty));

        return Ok(kanjiMeanings);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateCharacterRequest request)
    {
        var command = _mapper.Map<UpdateCharacterCommand>(request);
        command.Id = id;

        var character = _mapper.Map<Character>(command);

        return await Result.Create(command, General.UnProcessableRequest)
            .Bind(c => Mediator.Send(c))
            .Match<IActionResult>(() => CreatedAtAction("Get", new { id = character.Id }, character),
                () => NotFound("The character was not found or an error occurred during the update."));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        /*if (await _characterRepository.DeleteAsync(id))
            return Ok("The character was successfully deleted.");*/

        return NotFound("The character was not found or an error occurred during the delete.");
    }
}