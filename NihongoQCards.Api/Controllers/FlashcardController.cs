using DanilvarKanji.Application.Flashcards.Commands;
using DanilvarKanji.Application.Flashcards.Queries;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Entities.Flashcards;
using DanilvarKanji.Shared.Domain.Params;
using DanilvarKanji.Shared.Requests.Flashcards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Authorize]
public class FlashcardController : ApiController
{
    private readonly UserManager<AppUser> _userManager;

    public FlashcardController(IMediator mediator, UserManager<AppUser> userManager) : base(mediator)
    {
        _userManager = userManager;
    }

    [HttpPost("Collection")]
    [ProducesResponseType(typeof(FlashcardCollection), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCollectionAsync([FromBody] CreateFlashcardCollectionRequest request)
    {
        AppUser? user = await GetCurrentUser(_userManager);

        var result = await Mediator.Send(
            new CreateFlashcardCollectionCommand
            (
                Name: request.Name,
                Flashcards: request.Flashcards,
                AppUser: user!
            )
        );

        if (result.IsFailure)
            return HandleFailure(result);

        return Ok();
    }

    [HttpPut("Collection")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCollectionAsync([FromBody] UpdateFlashcardCollectionRequest request)
    {
        AppUser? user = await GetCurrentUser(_userManager);

        var result = await Mediator.Send(
            new UpdateFlashcardCollectionCommand
            (
                request.CollectionToUpdateId,
                user!,
                request.Name,
                request.Flashcards
            )
        );
        
        if (result.IsSuccess)
            return Ok(result.Value);

        return HandleFailure(result);
    }

    [HttpGet("Collections")]
    [ProducesResponseType(typeof(IEnumerable<FlashcardCollection>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ListAllCollectionsAsync([FromQuery] PaginationParams paginationParams)
    {
        AppUser? user = await GetCurrentUser(_userManager);

        var collections =
            await Mediator.Send(new ListFlashcardCollectionsQuery(paginationParams, user!));

        return collections.Any() ? Ok(collections) : NoContent();
    }

    [HttpGet("Collection/{id}")]
    [ProducesResponseType(typeof(FlashcardCollection), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetCollectionAsync(string id)
    {
        AppUser? user = await GetCurrentUser(_userManager);
        FlashcardCollection? collection = await Mediator.Send(new GetFlashcardCollectionQuery(id, user!));

        return collection != null ? Ok(collection) : NotFound(General.NotFound("Flashcard Collection"));
    }
}