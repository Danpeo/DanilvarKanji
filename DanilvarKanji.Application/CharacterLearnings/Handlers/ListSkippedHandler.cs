using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Responses.CharacterLearning;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

public class ListSkippedHandler : IRequestHandler<ListSkippedQuery, IEnumerable<CharacterLearning>>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;

    public ListSkippedHandler(ICharacterLearningRepository characterLearningRepository)
    {
        _characterLearningRepository = characterLearningRepository;
    }


    public async Task<IEnumerable<CharacterLearning>> Handle(ListSkippedQuery request,
        CancellationToken cancellationToken)
    {
        if (await _characterLearningRepository.AnyExistAsync())
        {
            return await _characterLearningRepository.ListSkippedAsync(request.PaginationParams, request.AppUser);
        }

        return Enumerable.Empty<CharacterLearning>();
    }
}