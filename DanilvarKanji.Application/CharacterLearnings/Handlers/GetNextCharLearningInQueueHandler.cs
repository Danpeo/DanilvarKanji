using AutoMapper;
using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Responses.CharacterLearning;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

// ReSharper disable once UnusedType.Global
public class GetNextCharLearningInQueueHandler : IRequestHandler<GetNextToReviewInQueueQuery, GetCharacterLearningBaseInfoResponse?>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;
    private readonly IMapper _mapper;

    public GetNextCharLearningInQueueHandler(ICharacterLearningRepository characterLearningRepository, IMapper mapper)
    {
        _characterLearningRepository = characterLearningRepository;
        _mapper = mapper;
    }

    public async Task<GetCharacterLearningBaseInfoResponse?> Handle(GetNextToReviewInQueueQuery request, CancellationToken cancellationToken)
    {
        if (await _characterLearningRepository.AnyToReview(request.AppUser))
        {
            CharacterLearning? learning = await _characterLearningRepository.GetNextInReviewQueue(request.AppUser);
            return _mapper.Map<GetCharacterLearningBaseInfoResponse>(learning);
        }

        return null;
    }
}