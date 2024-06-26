using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using MediatR;

namespace DanilvarKanji.Application.Exercises.Commands;

public class CreateExerciseCommand : IRequest<Result<string>>
{
    public CreateExerciseCommand(
        string characterId,
        AppUser appUser,
        bool isCorrect,
        ExerciseType exerciseType,
        ExerciseSubject exerciseSubject
    )
    {
        CharacterId = characterId;
        AppUser = appUser;
        IsCorrect = isCorrect;
        ExerciseType = exerciseType;
        ExerciseSubject = exerciseSubject;
    }

    public string Id { get; set; } = "";
    public string CharacterId { get; set; }

    public AppUser AppUser { get; set; }

    public bool IsCorrect { get; set; }

    public ExerciseType ExerciseType { get; set; } = ExerciseType.Point;
    public ExerciseSubject ExerciseSubject { get; set; }
}