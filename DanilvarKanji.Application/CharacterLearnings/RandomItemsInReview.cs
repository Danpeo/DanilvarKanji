namespace DanilvarKanji.Application.CharacterLearnings;

public class RandomItemsInReview
{
    public RandomItemsInReview(List<string> randomItems, string correctMeaning)
    {
        RandomItems = randomItems;
        CorrectMeaning = correctMeaning;
    }

    public List<string>? RandomItems { get; set; }

    public string? CorrectMeaning { get; set; }
}