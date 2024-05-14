using AutoMapper;
using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Shared.Responses.Character;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class GetNextInLearnQueueHandler
    : IRequestHandler<GetNextInLearnQueueQuery, CharacterResponseBase?>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;

    public GetNextInLearnQueueHandler(ICharacterRepository characterRepository, IMapper mapper)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
    }

    public async Task<CharacterResponseBase?> Handle(
        GetNextInLearnQueueQuery request,
        CancellationToken cancellationToken
    )
    {
        if (await _characterRepository.AnyInLearnQueueAsync(request.AppUser))
        {
            Character? character = await _characterRepository.GetNextInLearnQueueAsync(request.AppUser);
            return _mapper.Map<CharacterResponseBase>(character);
        }

        return null;
    }
}