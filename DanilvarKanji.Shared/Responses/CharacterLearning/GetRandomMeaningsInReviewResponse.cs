namespace DanilvarKanji.Shared.Responses.CharacterLearning;

public class GetRandomMeaningsInReviewResponse
{
    public List<string> RandomMeanings { get; set; }

    public string CorrectMeaning { get; set; }

    public GetRandomMeaningsInReviewResponse(List<string> randomMeanings, string correctMeaning)
    {
        RandomMeanings = randomMeanings;
        CorrectMeaning = correctMeaning;
    }
}