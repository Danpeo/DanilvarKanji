using DanilvarKanji.Application.CharacterLearnings.Commands;
using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.Params;
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
    private readonly UserManager<AppUser> _userManager;

    public CharacterLearningController(IMediator mediator, UserManager<AppUser> userManager) :
        base(mediator)
    {
        _userManager = userManager;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CharacterLearning), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCharacterLearningRequest request)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        var command = new CreateCharacterLearningCommand(user!, request.CharacterId, request.LearningState);

        var result = await Mediator.Send(command);

        if (result.IsFailure)
            return HandleFailure(result);

        command.Id = result.Value;
        return CreatedAtAction("Get", new { id = result.Value }, command);
    }

    [HttpPatch("ToggleSkipState")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ToggleSkipStateAsync([FromBody] string id)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        var command = new ToggleSkipStateCommand(id, user!);

        var result = await Mediator.Send(command);

        if (result.IsSuccess)
            return Ok(result.Value);

        return HandleFailure(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        CharacterLearning? learning = await Mediator.Send(new GetCharacterLearningQuery(id, user!));

        return learning is not null ? Ok(learning) : NotFound("Character Learning was not found");
    }

    [HttpGet("LearnQueue")]
    [ProducesResponseType(typeof(IEnumerable<CharacterLearning>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ListLearnQueueAsync([FromQuery] PaginationParams paginationParams,
        bool listOnlyDayDosage = false)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        IEnumerable<CharacterLearning> characters =
            await Mediator.Send(new ListLearnQueueQuery(paginationParams, user!.JlptLevel, user, listOnlyDayDosage));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [HttpGet("Skipped")]
    [ProducesResponseType(typeof(IEnumerable<CharacterLearning>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ListSkippedAsync([FromQuery] PaginationParams paginationParams)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        var characters = await Mediator.Send(new ListSkippedQuery(paginationParams, user!));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [HttpGet("CompletelyLearned")]
    [ProducesResponseType(typeof(IEnumerable<CharacterLearning>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ListCompletelyLearnedAsync([FromQuery] PaginationParams paginationParams)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        var characters = await Mediator.Send(new ListCompletelyLearnedQuery(paginationParams, user!));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [HttpGet("ReviewQueue")]
    [ProducesResponseType(typeof(IEnumerable<CharacterLearningResponseBase>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ListReviewQueueAsync([FromQuery] PaginationParams paginationParams)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        var characters =
            await Mediator.Send(new ListCurrentReviewQuery(paginationParams, user!));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [HttpGet("FutureReviewQueue")]
    [ProducesResponseType(typeof(IEnumerable<CharacterLearningResponseBase>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ListFutureReviewQueueAsync([FromQuery] PaginationParams paginationParams)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        var characters =
            await Mediator.Send(new ListFutureReviewQuery(paginationParams, user!));

        return characters.Any() ? Ok(characters) : NoContent();
    }

    [HttpGet("GetNextInReviewQueue")]
    [ProducesResponseType(typeof(CharacterLearningResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetNextInReviewQueueAsync()
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        CharacterLearningResponseBase? learning = await Mediator.Send(new GetNextToReviewInQueueQuery(user!));

        if (learning is not null)
            return Ok(learning);

        return NoContent();
    }

    [HttpGet("GetRandomMeaningsInReview")]
    [ProducesResponseType(typeof(RandomItemsInReviewResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRandomMeaningsInReviewAsync(string characterId, Culture culture = Culture.EnUS,
        int qty = 4)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        var result = await Mediator
            .Send(new GetRandomMeaningsInReviewQuery(characterId, user!, culture, qty));

        if (string.IsNullOrEmpty(result?.CorrectMeaning) || result.RandomItems == null || !result.RandomItems.Any())
            return NotFound(CharLearning.NotFoundInReview);

        var response = new RandomItemsInReviewResponse(result.RandomItems, result.CorrectMeaning);

        return result.RandomItems.Any() ? Ok(response) : NoContent();
    }

    [HttpGet("GetRandomKunReadingsInReview")]
    [ProducesResponseType(typeof(RandomItemsInReviewResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRandomKunReadingsInReviewAsync(string characterId, int qty = 4)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        var (random, correct) = await Mediator
            .Send(new GetRandomKunReadingsInReviewQuery(characterId, user!, qty));

        if (string.IsNullOrEmpty(correct))
            return NotFound(CharLearning.NotFoundInReview);

        var response = new RandomItemsInReviewResponse(random, correct);

        return random.Any() ? Ok(response) : NoContent();
    }

    [HttpGet("GetRandomOnReadingsInReview")]
    [ProducesResponseType(typeof(RandomItemsInReviewResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRandomOnReadingsInReviewAsync(string characterId, int qty = 4)
    {
        AppUser? user = await _userManager.GetUserAsync(User);

        var (random, correct) = await Mediator
            .Send(new GetRandomOnReadingsInReviewQuery(characterId, user!, qty));

        if (string.IsNullOrEmpty(correct))
            return NotFound(CharLearning.NotFoundInReview);

        var response = new RandomItemsInReviewResponse(random, correct);

        return random.Any() ? Ok(response) : NoContent();
    }
}