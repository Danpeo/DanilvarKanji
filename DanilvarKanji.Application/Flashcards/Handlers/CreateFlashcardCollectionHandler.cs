using DanilvarKanji.Application.Flashcards.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities.Flashcards;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Flashcards.Handlers;

public class CreateFlashcardCollectionHandler
    : IRequestHandler<CreateFlashcardCollectionCommand, Result<string>>
{
    private readonly IFlashcardRepository _flashcardRepository;
    private readonly ILogger<CreateFlashcardCollectionHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFlashcardCollectionHandler(
        IUnitOfWork unitOfWork,
        IFlashcardRepository flashcardRepository,
        ILogger<CreateFlashcardCollectionHandler> logger
    )
    {
        _unitOfWork = unitOfWork;
        _flashcardRepository = flashcardRepository;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(
        CreateFlashcardCollectionCommand request,
        CancellationToken cancellationToken
    )
    {
        var collection = new FlashcardCollection(
            request.Name,
            request.Flashcards,
            request.AppUser
        );

        _flashcardRepository.CreateCollection(collection);
        if (await _unitOfWork.CompleteAsync())
        {
            _logger.LogInformation("CREATED collection: {@colleciton}", collection);
            return Result.Success(collection.Id);
        }

        _logger.LogInformation("FAILED to create collection: {@colleciton}", collection);
        return Result.Failure<string>(General.UnProcessableRequest);
    }
}