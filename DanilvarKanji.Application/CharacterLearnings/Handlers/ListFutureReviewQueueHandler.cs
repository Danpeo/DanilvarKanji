using AutoMapper;
using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Responses.CharacterLearning;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

public class ListFutureReviewQueueHandler :
    IRequestHandler<
        ListFutureReviewQuery,
        IEnumerable<GetCharacterLearningBaseInfoResponse>>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ListFutureReviewQueueHandler> _logger;

    public ListFutureReviewQueueHandler
    (
        ICharacterLearningRepository characterLearningRepository,
        IMapper mapper,
        ILogger<ListFutureReviewQueueHandler> logger
    )
    {
        _characterLearningRepository = characterLearningRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetCharacterLearningBaseInfoResponse>> Handle(ListFutureReviewQuery request,
        CancellationToken cancellationToken)
    {
        if (await _characterLearningRepository.AnyToReview(request.AppUser))
        {
            var charLearnings =
                await _characterLearningRepository.ListFutureReviewQueueAsync(request.PaginationParams,
                    request.AppUser);

            _logger.LogInformation("Character Learnings: {@cl}", charLearnings);
            return _mapper.Map<IEnumerable<GetCharacterLearningBaseInfoResponse>>(charLearnings);
        }

        _logger.LogInformation("No character learnings");
        return Enumerable.Empty<GetCharacterLearningBaseInfoResponse>();
    }
}