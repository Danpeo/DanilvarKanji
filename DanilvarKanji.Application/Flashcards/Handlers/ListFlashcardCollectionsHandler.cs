using DanilvarKanji.Application.Flashcards.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities.Flashcards;
using MediatR;

namespace DanilvarKanji.Application.Flashcards.Handlers;

public class ListFlashcardCollectionsHandler
  : IRequestHandler<ListFlashcardCollectionsQuery, IEnumerable<FlashcardCollection>>
{
  private readonly IFlashcardRepository _flashcardRepository;

  public ListFlashcardCollectionsHandler(IFlashcardRepository flashcardRepository)
  {
    _flashcardRepository = flashcardRepository;
  }

  public async Task<IEnumerable<FlashcardCollection>> Handle(
    ListFlashcardCollectionsQuery request,
    CancellationToken cancellationToken
  )
  {
    if (await _flashcardRepository.AnyCollectionsExistAsync(request.AppUser))
      return await _flashcardRepository.ListCollectionsAsync(
        request.PaginationParams,
        request.AppUser
      );

    return Enumerable.Empty<FlashcardCollection>();
  }
}