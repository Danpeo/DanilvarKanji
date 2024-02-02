using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class ListCharactersHandler : IRequestHandler<ListCharactersQuery, IEnumerable<Character>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ILogger<ListCharactersHandler> _logger;

    public ListCharactersHandler(ICharacterRepository characterRepository, ILogger<ListCharactersHandler> logger)
    {
        _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
        _logger = logger;
    }

    public async Task<IEnumerable<Character>> Handle(ListCharactersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing List Characters Query: {@request}, {@dt}"
            , request, DateTime.UtcNow);
        
        if (await _characterRepository.AnyExistAsync())
        {
            return await _characterRepository.ListAsync(request.PaginationParams);
        }

        _logger.LogInformation("No Characters - {@dt}", DateTime.UtcNow);
        return Enumerable.Empty<Character>();
    }
}