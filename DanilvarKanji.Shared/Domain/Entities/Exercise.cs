using DanilvarKanji.Shared.Domain.Enumerations;

namespace DanilvarKanji.Shared.Domain.Entities;

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