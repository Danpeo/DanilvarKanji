using AutoMapper;
using DanilvarKanji.Application.CharacterLearnings.Commands;
using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Requests.CharacterLearnings;
using DanilvarKanji.Shared.Responses.CharacterLearning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers.Characters;

[Authorize]
public class CharacterLearningController : ApiController
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public CharacterLearningController(IMediator mediator, IMapper mapper, UserManager<AppUser> userManager) :
        base(mediator)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCharacterLearningRequest? request)
    {
        //var command = _mapper.Map<CreateCharacterLearningCommand>(request);

        AppUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
            return Unauthorized();

        var command = new CreateCharacterLearningCommand(user, request.CharacterId, request.LearningState);


        return await Result.Create(command, General.UnProcessableRequest)
            .Bind(c => Mediator.Send(c))
            .Match(() => CreatedAtAction("Get", new { id = request.CharacterId }),
                BadRequest);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
            return Unauthorized();

        CharacterLearning? learning = await Mediator.Send(new GetCharacterLearningQuery(id, user));

        return learning is not null ? Ok(learning) : NotFound("Character Learning was not found");
    }

    [HttpGet("LearnQueue")]
    public async Task<IActionResult> ListLearnQueueAsync([FromQuery] PaginationParams paginationParams)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
            return Unauthorized();

        IEnumerable<CharacterLearning> characters =
            await Mediator.Send(new ListLearnQueueQuery(paginationParams, user.JlptLevel, user));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [HttpGet("ReviewQueue")]
    public async Task<IActionResult> ListReviewQueueAsync([FromQuery] PaginationParams paginationParams)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
            return Unauthorized();

        var characters =
            await Mediator.Send(new ListCharacterReviewQuery(paginationParams, user));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [HttpGet("GetNextInReviewQueue")]
    public async Task<IActionResult> GetNextInReviewQueueAsync()
    {
        AppUser? user = await _userManager.GetUserAsync(User);
        if (user is null)
            return Unauthorized();

        GetCharacterLearningBaseInfoResponse? learning = await Mediator.Send(new GetNextToReviewInQueueQuery(user));

        if (learning is not null)
            return Ok(learning);

        return NotFound("Character learning was not fount");
    }
}