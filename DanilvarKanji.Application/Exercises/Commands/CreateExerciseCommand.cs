using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Primitives.Result;
using MediatR;

namespace DanilvarKanji.Application.Exercises.Commands;

public class CreateExerciseCommand : IRequest<Result>
{
    public string Id { get; set; }
    public string CharacterId { get; set; }

    public AppUser AppUser { get; set; }

    public bool IsCorrect { get; set; }

    public ReviewType ReviewType { get; set; } = ReviewType.Point;
    public ExerciseType ExerciseType { get; set; }


    public CreateExerciseCommand(string characterId, AppUser appUser, bool isCorrect, ReviewType reviewType, ExerciseType exerciseType)
    {
        Id = Guid.NewGuid().ToString("N");
        CharacterId = characterId;
        AppUser = appUser;
        IsCorrect = isCorrect;
        ReviewType = reviewType;
        ExerciseType = exerciseType;
    }
}