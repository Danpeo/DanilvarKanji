public class RandomItemsInReview
{
    public List<string>? RandomItems { get; set; }

    public string? CorrectMeaning { get; set; }

    public RandomItemsInReview(List<string> randomItems, string correctMeaning)
    {
        RandomItems = randomItems;
        CorrectMeaning = correctMeaning;
    }
}