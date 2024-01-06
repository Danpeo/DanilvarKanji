namespace DanilvarKanji.Shared.Responses.CharacterLearning;

public class GetRandomMeaningsInReviewResponse
{
    public IEnumerable<string> RandomMeanings { get; set; }

    public string CorrectMeaning { get; set; }

    public GetRandomMeaningsInReviewResponse(IEnumerable<string> randomMeanings, string correctMeaning)
    {
        RandomMeanings = randomMeanings;
        CorrectMeaning = correctMeaning;
    }
}