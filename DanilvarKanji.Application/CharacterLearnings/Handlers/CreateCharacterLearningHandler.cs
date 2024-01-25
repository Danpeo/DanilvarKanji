using DanilvarKanji.Application.CharacterLearnings.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

// ReSharper disable once UnusedType.Global
public class CreateCharacterLearningHandler : IRequestHandler<CreateCharacterLearningCommand, Result<string>>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCharacterLearningHandler(ICharacterLearningRepository characterLearningRepository,
        IUnitOfWork unitOfWork, ICharacterRepository characterRepository)
    {
        _characterLearningRepository = characterLearningRepository;
        _unitOfWork = unitOfWork;
        _characterRepository = characterRepository;
    }

    public async Task<Result<string>> Handle(CreateCharacterLearningCommand request, CancellationToken cancellationToken)
    {
        Character? character = await _characterRepository.GetAsync(request.CharacterId);

        var characterLearning = new CharacterLearning()
        {
            Id = request.Id,
            AppUser = request.AppUser,
            Character = character ?? new Character(),
            LearningProgress = new LearningProgress(),
            LearningState = request.LearningState
        };

        _characterLearningRepository.Create(characterLearning);
        if (await _unitOfWork.CompleteAsync())
            return Result.Success(characterLearning.Id);

        return Result.Failure<string>(General.UnProcessableRequest);
    }
}