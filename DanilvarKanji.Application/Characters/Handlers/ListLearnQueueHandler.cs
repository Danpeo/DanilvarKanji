using AutoMapper;
using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Params;
using DanilvarKanji.Shared.Responses.Character;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class ListLearnQueueHandler : IRequestHandler<ListLearnQueueQuery, IEnumerable<GetCharacterBaseInfoResponse>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;

    public ListLearnQueueHandler(ICharacterRepository characterRepository, IMapper mapper)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCharacterBaseInfoResponse>> Handle(ListLearnQueueQuery request,
        CancellationToken cancellationToken)
    {
        if (await _characterRepository.AnyInLearnQueueAsync(request.AppUser))
        {
            IEnumerable<Character> characters;
            if (request.listOnlyDayDosage)
            {
                var paginationParams = new PaginationParams(1, request.AppUser.QtyOfCharsForLearningForDay);

                characters = await _characterRepository.ListLearnQueueAsync(paginationParams, request.AppUser,
                    request.JlptLevel);
            }
            else
            {
                characters = await _characterRepository
                    .ListLearnQueueAsync(request.PaginationParams, request.AppUser,
                        request.JlptLevel);
            }

            return _mapper.Map<IEnumerable<GetCharacterBaseInfoResponse>>(characters);
        }

        return Enumerable.Empty<GetCharacterBaseInfoResponse>();
    }
}