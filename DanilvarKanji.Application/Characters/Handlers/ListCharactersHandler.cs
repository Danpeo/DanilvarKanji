using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class ListCharactersHandler : IRequestHandler<ListCharactersQuery, IEnumerable<Character>>
{
    private readonly ICharacterRepository _characterRepository;
    
    public ListCharactersHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
    }
    
    public async Task<IEnumerable<Character>> Handle(ListCharactersQuery request, CancellationToken cancellationToken)
    {
        
        if (await _characterRepository.AnyExistAsync())
            return await _characterRepository.ListAsync(request.PaginationParams);

        return Enumerable.Empty<Character>();
    }
}