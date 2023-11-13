namespace DanilvarKanji.Domain.Entities.Exercises;

public class MeaningExercise : Excercise
{
    public MeaningExercise(Character character, AppUser appUser, bool isCorrect, DateTime excerciseDateTime) : base(
        character, appUser, isCorrect, excerciseDateTime)
    {
    }
}