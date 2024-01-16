namespace DanilvarKanji.Shared.Responses.CharacterLearning;

public class GetRandomItemsInReviewResponse
{
    public List<string> RandomItems { get; set; }

    public string CorrectMeaning { get; set; }

    public GetRandomItemsInReviewResponse(List<string> randomItems, string correctMeaning)
    {
        RandomItems = randomItems;
        CorrectMeaning = correctMeaning;
    }
}