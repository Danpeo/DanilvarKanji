using DanilvarKanji.Application.Flashcards.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Flashcards.Handlers;

public class DeleteCollectionHandler : IRequestHandler<DeleteCollectionCommand, Result<string>>
{
  private readonly IFlashcardRepository _flashcardRepository;
  private readonly ILogger<DeleteCollectionHandler> _logger;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteCollectionHandler(
    IFlashcardRepository flashcardRepository,
    IUnitOfWork unitOfWork,
    ILogger<DeleteCollectionHandler> logger
  )
  {
    _flashcardRepository = flashcardRepository;
    _unitOfWork = unitOfWork;
    _logger = logger;
  }

  public async Task<Result<string>> Handle(
    DeleteCollectionCommand request,
    CancellationToken cancellationToken
  )
  {
    if (await _flashcardRepository.ExistAsync(request.CollectionId, request.AppUser))
    {
      await _flashcardRepository.DeleteAsync(request.CollectionId, request.AppUser);

      if (await _unitOfWork.CompleteAsync())
      {
        _logger.LogInformation(
          "DELETED Collection: {@collection} for {@user}",
          request.CollectionId,
          request.AppUser
        );

        return Result.Success(request.CollectionId);
      }
    }

    _logger.LogError(
      "FAILED to DELETE Collection: {@collection} for {@user}",
      request.CollectionId,
      request.AppUser
    );
    return Result.Failure<string>(General.NotFound("Collection"));
  }
}