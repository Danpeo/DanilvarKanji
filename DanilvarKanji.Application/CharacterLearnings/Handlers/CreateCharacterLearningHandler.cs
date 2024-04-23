using DanilvarKanji.Application.CharacterLearnings.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

// ReSharper disable once UnusedType.Global
public class CreateCharacterLearningHandler : IRequestHandler<CreateCharacterLearningCommand, Result<string>>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCharacterLearningHandler> _logger;

    public CreateCharacterLearningHandler(ICharacterLearningRepository characterLearningRepository,
        IUnitOfWork unitOfWork, ICharacterRepository characterRepository,
        ILogger<CreateCharacterLearningHandler> logger)
    {
        _characterLearningRepository = characterLearningRepository;
        _unitOfWork = unitOfWork;
        _characterRepository = characterRepository;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(CreateCharacterLearningCommand request,
        CancellationToken cancellationToken)
    {
        Character? character = await _characterRepository.GetAsync(request.CharacterId);

        _logger.LogInformation("Getted character: {@character}", character);

        var characterLearning = new CharacterLearning()
        {
            Id = request.Id,
            AppUser = request.AppUser,
            Character = character ?? new Character(),
            /*
            LearningProgress = new LearningProgress(),
            */
            LearningState = request.LearningState
        };

        _characterLearningRepository.Create(characterLearning);
        if (await _unitOfWork.CompleteAsync())
        {
            _logger.LogInformation("Created character learning: {@characterLearning}", characterLearning);
            return Result.Success(characterLearning.Id);
        }

        _logger.LogError("Failed to create character learning: {@characterLearning}", characterLearning);
        return Result.Failure<string>(General.UnProcessableRequest);
    }
}