using DanilvarKanji.Application.Exercises.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.Exercises.Handlers;

// ReSharper disable once UnusedType.Global
public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, Result<string>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateExerciseHandler(
        IExerciseRepository exerciseRepository,
        IUnitOfWork unitOfWork,
        ICharacterRepository characterRepository
    )
    {
        _exerciseRepository = exerciseRepository;
        _unitOfWork = unitOfWork;
        _characterRepository = characterRepository;
    }

    public async Task<Result<string>> Handle(
        CreateExerciseCommand request,
        CancellationToken cancellationToken
    )
    {
        Character? character = await _characterRepository.GetAsync(request.CharacterId);

        var exercise = new Exercise
        {
            Character = character ?? new Character(),
            AppUser = request.AppUser,
            ExcerciseDateTime = DateTime.UtcNow,
            ExerciseType = request.ExerciseType,
            ExerciseSubject = request.ExerciseSubject,
            IsCorrect = request.IsCorrect
        };

        _exerciseRepository.Create(exercise);
        if (await _unitOfWork.CompleteAsync())
            return Result.Success(exercise.Id);

        return Result.Failure<string>(General.UnProcessableRequest);
    }
}