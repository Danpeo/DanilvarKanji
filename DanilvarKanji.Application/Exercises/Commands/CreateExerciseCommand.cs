using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Enumerations;
using MediatR;

namespace DanilvarKanji.Application.Exercises.Commands;

public class CreateExerciseCommand : IRequest<Result>
{
    public string Id { get; set; }
    public string CharacterId { get; set; }

    public AppUser AppUser { get; set; }

    public bool IsCorrect { get; set; }

    public ExerciseType ExerciseType { get; set; } = ExerciseType.Point;
    public ExerciseSubject ExerciseSubject { get; set; }


    public CreateExerciseCommand(string characterId, AppUser appUser, bool isCorrect, ExerciseType exerciseType, ExerciseSubject exerciseSubject)
    {
        Id = Guid.NewGuid().ToString("N");
        CharacterId = characterId;
        AppUser = appUser;
        IsCorrect = isCorrect;
        ExerciseType = exerciseType;
        ExerciseSubject = exerciseSubject;
    }
}