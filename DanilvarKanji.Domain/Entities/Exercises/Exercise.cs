using Danilvar.Entity;
using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Domain.Entities.Exercises;

public class Exercise : Entity
{
    public Character Character { get; set; }
    public AppUser AppUser { get; set; }
    public bool IsCorrect { get; set; }
    public ExerciseType ExerciseType { get; set; } = ExerciseType.Point;
    public DateTime ExcerciseDateTime { get; set; }

    public Exercise(Character character, AppUser appUser, bool isCorrect, DateTime excerciseDateTime)
    {
        Character = character;
        AppUser = appUser;
        IsCorrect = isCorrect;
        ExcerciseDateTime = excerciseDateTime;
    }
}