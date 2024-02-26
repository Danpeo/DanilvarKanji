using DanilvarKanji.Application.Flashcards.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Entities.Flashcards;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Flashcards.Handlers;

public class GetFlashcardCollectionHandler : IRequestHandler<GetFlashcardCollectionQuery, FlashcardCollection?>
{
    private readonly IFlashcardRepository _flashcardRepository;
    private readonly ILogger<GetFlashcardCollectionHandler> _logger;

    public GetFlashcardCollectionHandler(IFlashcardRepository flashcardRepository,
        ILogger<GetFlashcardCollectionHandler> logger)
    {
        _flashcardRepository = flashcardRepository;
        _logger = logger;
    }

    public async Task<FlashcardCollection?> Handle(GetFlashcardCollectionQuery request,
        CancellationToken cancellationToken)
    {
        if (await _flashcardRepository.ExistAsync(request.Id, request.AppUser))
        {
            var collection = await _flashcardRepository.GetCollectionAsync(request.Id, request.AppUser);
            _logger.LogInformation("Get Flashcard Collection: {@collection}", collection);
            return collection;
        }

        return default;
    }
}