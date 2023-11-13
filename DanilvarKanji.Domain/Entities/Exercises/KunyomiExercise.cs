namespace DanilvarKanji.Domain.Entities.Exercises;

public class KunyomiExercise : Excercise
{
    public KunyomiExercise(Character character, AppUser appUser, bool isCorrect, DateTime excerciseDateTime) : base(
        character, appUser, isCorrect, excerciseDateTime)
    {
    }
}