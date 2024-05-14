using DanilvarKanji.Application.Flashcards.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities.Flashcards;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Flashcards.Handlers;

public class UpdateFlashcardCollectionHandler
    : IRequestHandler<UpdateFlashcardCollectionCommand, Result<string>>
{
    private readonly IFlashcardRepository _flashcardRepository;
    private readonly ILogger<UpdateFlashcardCollectionHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFlashcardCollectionHandler(
        IFlashcardRepository flashcardRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateFlashcardCollectionHandler> logger
    )
    {
        _flashcardRepository = flashcardRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(
        UpdateFlashcardCollectionCommand request,
        CancellationToken cancellationToken
    )
    {
        if (await _flashcardRepository.ExistAsync(request.CollectionId, request.AppUser))
        {
            var collection = new FlashcardCollection(request.Name, request.Flashcards, request.AppUser);
            await _flashcardRepository.UpdateCollectionAsync(
                request.CollectionId,
                request.AppUser,
                collection
            );
            if (await _unitOfWork.CompleteAsync())
            {
                _logger.LogInformation(
                    "UPDATED Flashcard Collection: {@request}, {@dt}",
                    request,
                    DateTime.UtcNow
                );
                return Result.Success(request.CollectionId);
            }
        }

        _logger.LogError(
            "FAILED to Update Flashcard Collection: {@request}, {@dt}",
            request,
            DateTime.UtcNow
        );
        return Result.Failure<string>(General.UnProcessableRequest);
    }
}