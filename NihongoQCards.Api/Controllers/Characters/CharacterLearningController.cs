using AutoMapper;
using DanilvarKanji.Application.CharacterLearnings.Commands;
using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Requests.CharacterLearnings;
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

    public CharacterLearningController(IMediator mediator, IMapper mapper, UserManager<AppUser> userManager) : base(mediator)
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
        
        //var characterLearning = _mapper.Map<CharacterLearning>(command);

        return await Result.Create(command, General.UnProcessableRequest)
            .Bind(c => Mediator.Send(c))
            .Match(Ok, BadRequest);
    }

    [HttpGet("LearnQueue")]
    public async Task<IActionResult> ListLearnQueueAsync([FromServices] UserManager<AppUser> userManager,
        [FromQuery] PaginationParams paginationParams)
    {
        AppUser? user = await userManager.GetUserAsync(User);

        if (user is null)
            return Unauthorized();

        IEnumerable<CharacterLearning> characters =
            await Mediator.Send(new ListLearnQueueQuery(paginationParams, user.JlptLevel, user));

        return characters.Any() ? Ok(characters) : NoContent();
    }
}