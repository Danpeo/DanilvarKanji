using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.RepositoryAbstractions;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

public class ListCharactersHandler : IRequestHandler<ListCharactersQuery, IEnumerable<Character>>
{
    private readonly ICharacterRepository _characterRepository;

    public ListCharactersHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
    }
    
    public async Task<IEnumerable<Character>> Handle(ListCharactersQuery request, CancellationToken cancellationToken)
    {
        if (await _characterRepository.AnyExist())
            return await _characterRepository.ListAsync(request.PaginationParams);

        return Enumerable.Empty<Character>();
    }
}