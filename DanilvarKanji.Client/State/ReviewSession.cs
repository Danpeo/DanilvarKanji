using DanilvarKanji.Shared.Responses.Character;
using DanilvarKanji.Shared.Responses.Exercise;

namespace DanilvarKanji.Client.State;

public class ReviewSession
{
    public CharacterResponseResponseFull Character { get; set; }
    public List<ExerciseResponseFull?> Exercises { get; set; } = new();
}
