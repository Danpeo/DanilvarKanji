using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Domain.Shared.Entities;

public class Exercise
{
    public string Id { get; set; }
    public Character Character { get; set; }
    public AppUser AppUser { get; set; }
    public bool IsCorrect { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public ExerciseSubject ExerciseSubject { get; set; }
    public DateTime ExcerciseDateTime { get; set; } = DateTime.UtcNow;
}