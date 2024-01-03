using DanilvarKanji.Shared.Responses.Character;
using DanilvarKanji.Shared.Responses.Exercise;

namespace DanilvarKanji.Client.State;

public class ReviewSession
{
    public GetAllFromCharacterResponse Character { get; set; }
    public List<GetAllFromExerciseResponse?> Exercises { get; set; } = new();
}
