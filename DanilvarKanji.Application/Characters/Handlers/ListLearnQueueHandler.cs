using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.RepositoryAbstractions;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class ListLearnQueueHandler : IRequestHandler<ListLearnQueueQuery, IEnumerable<Character>>
{
    private readonly ICharacterRepository _characterRepository;

    public ListLearnQueueHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<IEnumerable<Character>> Handle(ListLearnQueueQuery request, CancellationToken cancellationToken)
    {
        if (await _characterRepository.AnyExist())
            return await _characterRepository.ListLearnQueueAsync(request.PaginationParams, request.JlptLevel);

        return Enumerable.Empty<Character>();    }
}