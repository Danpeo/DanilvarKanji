using AutoMapper;
using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Shared.Responses.Character;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class ListLearnQueueHandler
    : IRequestHandler<ListLearnQueueQuery, IEnumerable<CharacterResponseBase>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;

    public ListLearnQueueHandler(ICharacterRepository characterRepository, IMapper mapper)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CharacterResponseBase>> Handle(
        ListLearnQueueQuery request,
        CancellationToken cancellationToken
    )
    {
        if (await _characterRepository.AnyInLearnQueueAsync(request.AppUser))
        {
            IEnumerable<Character> characters;
            if (request.listOnlyDayDosage)
            {
                var paginationParams = new PaginationParams(1, request.AppUser.QtyOfCharsForLearningForDay);

                characters = _characterRepository.ListLearnQueue(
                    paginationParams,
                    request.AppUser,
                    request.JlptLevel
                );
            }
            else
            {
                characters = _characterRepository.ListLearnQueue(
                    request.PaginationParams,
                    request.AppUser,
                    request.JlptLevel
                );
            }

            return _mapper.Map<IEnumerable<CharacterResponseBase>>(characters);
        }

        return Enumerable.Empty<CharacterResponseBase>();
    }
}