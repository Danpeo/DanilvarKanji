using AutoMapper;
using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Responses.CharacterLearning;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

public class ListFutureReviewQueueHandler
  : IRequestHandler<ListFutureReviewQuery, IEnumerable<CharacterLearningResponseBase>>
{
  private readonly ICharacterLearningRepository _characterLearningRepository;
  private readonly ILogger<ListFutureReviewQueueHandler> _logger;
  private readonly IMapper _mapper;

  public ListFutureReviewQueueHandler(
    ICharacterLearningRepository characterLearningRepository,
    IMapper mapper,
    ILogger<ListFutureReviewQueueHandler> logger
  )
  {
    _characterLearningRepository = characterLearningRepository;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<IEnumerable<CharacterLearningResponseBase>> Handle(
    ListFutureReviewQuery request,
    CancellationToken cancellationToken
  )
  {
    if (await _characterLearningRepository.AnyToReview(request.AppUser))
    {
      var charLearnings = await _characterLearningRepository.ListFutureReviewQueueAsync(
        request.PaginationParams,
        request.AppUser
      );

      _logger.LogInformation("Character Learnings: {@cl}", charLearnings);
      return _mapper.Map<IEnumerable<CharacterLearningResponseBase>>(charLearnings);
    }

    _logger.LogInformation("No character learnings");
    return Enumerable.Empty<CharacterLearningResponseBase>();
  }
}