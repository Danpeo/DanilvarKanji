using DanilvarKanji.Application.Flashcards.Commands;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Requests.Flashcards;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

public class FlashcardController : ApiController
{
    private readonly UserManager<AppUser> _userManager;

    public FlashcardController(IMediator mediator, UserManager<AppUser> userManager) : base(mediator)
    {
        _userManager = userManager;
    }

    [HttpPost("FlashcardCollection")]
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
    
}