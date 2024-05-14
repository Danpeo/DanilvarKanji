namespace DanilvarKanji.Shared.Responses.CharacterLearning;

public class RandomItemsInReviewResponse
{
    public RandomItemsInReviewResponse(List<string> randomItems, string correctMeaning)
    {
        RandomItems = randomItems;
        CorrectMeaning = correctMeaning;
    }

    public List<string> RandomItems { get; set; }

    public string CorrectMeaning { get; set; }
}