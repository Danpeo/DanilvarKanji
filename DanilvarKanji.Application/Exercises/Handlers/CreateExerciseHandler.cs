using DanilvarKanji.Application.Exercises.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Entities.Exercises;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.Exercises.Handlers;

// ReSharper disable once UnusedType.Global
public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, Result>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICharacterRepository _characterRepository;

    public CreateExerciseHandler(IExerciseRepository exerciseRepository, IUnitOfWork unitOfWork, ICharacterRepository characterRepository)
    {
        _exerciseRepository = exerciseRepository;
        _unitOfWork = unitOfWork;
        _characterRepository = characterRepository;
    }

    public async Task<Result> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        Character? character = await _characterRepository.GetAsync(request.CharacterId);

        var exercise = new Exercise
        {
            Id = request.Id,
            Character = character ?? new Character(),
            AppUser = request.AppUser,
            ExcerciseDateTime = DateTime.UtcNow,
            ReviewType = request.ReviewType,
            ExerciseType = request.ExerciseType,
            IsCorrect = request.IsCorrect
        };
        
        _exerciseRepository.Create(exercise);
        if (await _unitOfWork.CompleteAsync())
            return Result.Success();

        return Result.Failure(General.UnProcessableRequest);
    }
}