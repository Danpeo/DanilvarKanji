using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.RepositoryAbstractions;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class GetCharacterHandler : IRequestHandler<GetCharacterQuery, Character?>
{
    private readonly ICharacterRepository _characterRepository;

    public GetCharacterHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
    }

    public async Task<Character?> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
    {
        if (await _characterRepository.Exist(request.Id))
            return await _characterRepository.GetAsync(request.Id);

        return null;
    }
}