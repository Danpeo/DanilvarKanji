namespace DanilvarKanji.Domain.Entities.Exercises;

public class OnyomiExercise : Excercise
{
    public OnyomiExercise(Character character, AppUser appUser, bool isCorrect, DateTime excerciseDateTime) : base(
        character, appUser, isCorrect, excerciseDateTime)
    {
    }
}