using AutoMapper;
using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.Params;
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

    public CharacterController(IMediator mediator, IMapper mapper, UserManager<AppUser> userManager)
        : base(mediator)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    [Authorize(Roles = $"{UserRole.Admin},{UserRole.SuperAdmin}")]
    [HttpPost]
    [ProducesResponseType(typeof(Character), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateAsync([FromBody] CharacterRequest? request)
    {
        var command = _mapper.Map<CreateCharacterCommand>(request);

        var result = await Mediator.Send(command);

        if (result.IsFailure)
            return HandleFailure(result);

        return CreatedAtAction("Get", new { id = result.Value }, command);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateCharacterRequest request)
    {
        var command = _mapper.Map<UpdateCharacterCommand>(request);
        command.Id = id;
        var result = await Mediator.Send(command);

        if (result.IsFailure)
            return HandleFailure(result);

        return Ok(command);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Character>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListAsync([FromQuery] PaginationParams paginationParams)
    {
        var characters = await Mediator.Send(
            new ListCharactersQuery(paginationParams)
        );

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [Authorize]
    [HttpGet("LearnQueue")]
    [ProducesResponseType(typeof(IEnumerable<CharacterResponseBase>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListLearnQueueAsync(
        [FromQuery] PaginationParams paginationParams,
        bool listOnlyDayDosage = false
    )
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
            return Unauthorized();

        var characters = await Mediator.Send(
            new ListLearnQueueQuery(paginationParams, user.JlptLevel, user, listOnlyDayDosage)
        );

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [Authorize]
    [HttpGet("GetNextInLearnQueue")]
    [ProducesResponseType(typeof(CharacterResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetNextInLearnQueueAsync()
    {
        AppUser? user = await _userManager.GetUserAsync(User);
        if (user is null)
            return Unauthorized();

        CharacterResponseBase? character = await Mediator.Send(new GetNextInLearnQueueQuery(user));

        if (character is not null)
            return Ok(character);

        return NoContent();
    }

    [HttpGet("{searchTerm}:Search")]
    [ProducesResponseType(typeof(IEnumerable<Character>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SearchAsync(
        [FromQuery] PaginationParams paginationParams,
        string searchTerm = "any"
    )
    {
        var characters = await Mediator.Send(
            new SearchCharactersQuery(searchTerm, paginationParams)
        );

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [HttpGet("{id}:Child")]
    public async Task<IActionResult> ListChildCharacters(string id)
    {
        var characters = await Mediator.Send(new ListChildCharactersQuery(id));

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
    public async Task<IActionResult> GetKanjiMeaningsByPriorityAsync(
        string id,
        Culture culture = Culture.EnUS,
        int takeQty = int.MaxValue
    )
    {
        var kanjiMeanings = await Mediator.Send(
            new GetKanjiMeaningsByPriorityQuery(id, culture, takeQty)
        );

        return kanjiMeanings.Any() ? Ok(kanjiMeanings) : NotFound();
    }

    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.SuperAdmin}")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(string id, Culture culture)
    {
        Result result = await Mediator.Send(new DeleteCharacterCommand(id)
        {
            Culture = culture
        });

        if (result.IsSuccess)
            return Ok();

        return HandleFailure(result);
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