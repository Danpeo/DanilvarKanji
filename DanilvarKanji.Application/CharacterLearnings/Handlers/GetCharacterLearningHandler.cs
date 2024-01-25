using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

// ReSharper disable once UnusedType.Global
public class GetCharacterLearningHandler : IRequestHandler<GetCharacterLearningQuery, CharacterLearning?>
{
    private readonly ICharacterLearningRepository _learningRepository;

    public GetCharacterLearningHandler(ICharacterLearningRepository learningRepository)
    {
        _learningRepository = learningRepository;
    }

    public async Task<CharacterLearning?> Handle(GetCharacterLearningQuery request, CancellationToken cancellationToken)
    {
        if (await _learningRepository.Exist(request.Id, request.AppUser))
            return await _learningRepository.GetAsync(request.Id, request.AppUser);

        return null;
    }
}