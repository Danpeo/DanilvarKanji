using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Domain.Entities.Exercises;

public class Exercise
{
    public string Id { get; set; }
    public Character Character { get; set; }
    public AppUser AppUser { get; set; }
    public bool IsCorrect { get; set; }
    public ReviewType ReviewType { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public DateTime ExcerciseDateTime { get; set; } = DateTime.UtcNow;
}