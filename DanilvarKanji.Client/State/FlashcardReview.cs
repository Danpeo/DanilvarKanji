namespace DanilvarKanji.Client.State;

public class FlashcardReview
{
    public string CollectionName { get; set; }

    public string ReviewTime { get; set; }

    public int Remembered { get; set; }

    public int Forgot { get; set; }

    public int RememberedPerfectly { get; set; }
    public int FlashcardReviewedCount { get; set; }

    public FlashcardReview()
    {
        CollectionName = "";
        ReviewTime = "00:00";
    }
    
    public FlashcardReview(string collectionName, string reviewTime, int remembered, int forgot, int rememberedPerfectly, int flashcardReviewedCount)
    {
        CollectionName = collectionName;
        ReviewTime = reviewTime;
        Remembered = remembered;
        Forgot = forgot;
        RememberedPerfectly = rememberedPerfectly;
        FlashcardReviewedCount = flashcardReviewedCount;
    }
}