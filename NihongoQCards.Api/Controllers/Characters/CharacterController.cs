using AutoMapper;
using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.Primitives;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Requests.Characters;
using DanilvarKanji.Shared.Responses.Character;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using EnumHelper = DanilvarKanji.Infrastructure.Common.EnumHelper;

namespace DanilvarKanji.Controllers.Characters;

public class CharacterController : ApiController
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public CharacterController(IMediator mediator, IMapper mapper, UserManager<AppUser> userManager) :
        base(mediator)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Character), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCharacterRequest? request)
    {
        var command = _mapper.Map<CreateCharacterCommand>(request);
        var character = _mapper.Map<Character>(command);

        return await Result.Create(command, General.UnProcessableRequest)
            .Bind(c => Mediator.Send(c))
            .Match(() => CreatedAtAction("Get", new { id = character.Id }, character), BadRequest);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Character>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListAsync([FromQuery] PaginationParams paginationParams)
    {
        IEnumerable<Character> characters = await Mediator.Send(new ListCharactersQuery(paginationParams));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [Authorize]
    [HttpGet("LearnQueue")]
    [ProducesResponseType(typeof(IEnumerable<GetCharacterBaseInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(typeof(GetCharacterBaseInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetNextInLearnQueueAsync()
    {
        AppUser? user = await _userManager.GetUserAsync(User);
        if (user is null)
            return Unauthorized();

        GetCharacterBaseInfoResponse? character = await Mediator.Send(new GetNextInLearnQueueQuery(user));

        if (character is not null)
            return Ok(character);

        return NoContent();
    }

    [HttpGet("{searchTerm}:Search")]
    [ProducesResponseType(typeof(IEnumerable<Character>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(typeof(Character), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(string id)
    {
        Character? character = await Mediator.Send(new GetCharacterQuery(id));

        if (character is not null)
            return Ok(character);

        return NotFound("Character with this ID was not found");
    }

    [HttpGet("{id}:KanjiMeanings")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetKanjiMeaningsByPriorityAsync(string id, Culture culture = Culture.EnUS,
        int takeQty = int.MaxValue)
    {
        IEnumerable<string> kanjiMeanings =
            await Mediator.Send(new GetKanjiMeaningsByPriorityQuery(id, culture, takeQty));

        return kanjiMeanings.Any() ? Ok(kanjiMeanings) : NotFound();
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

    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.SuperAdmin}")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        Result result = await Mediator.Send(new DeleteCharacterCommand(id));

        if (result.IsSuccess)
            return Ok();
        
        return NotFound($"{result.Error.Code} - '{result.Error.Message}'");
    }

    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.SuperAdmin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("All")]
    public async Task<IActionResult> DeleteAllAsync()
    {
        Result result = await Mediator.Send(new DeleteAllCharactersCommand());

        if (result.IsSuccess)
            return Ok();
        
        return NotFound($"{result.Error.Code} - '{result.Error.Message}'");
    }
}