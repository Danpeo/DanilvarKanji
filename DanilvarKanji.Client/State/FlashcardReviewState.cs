using Blazored.SessionStorage;

namespace DanilvarKanji.Client.State;

public class FlashcardReviewState : StateBase
{
    private const string Key = "FlashcardReview";
    private readonly ISessionStorageService _sessionStorageService;
    public FlashcardReview FlashcardReview { get; private set; } = new();

    public FlashcardReviewState(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public override async Task Init()
    {
        if (!IsInitialized)
        {
            FlashcardReview = await _sessionStorageService.GetItemAsync<FlashcardReview>(Key);
            IsInitialized = true;
        }
    }
    
    public async Task NewReviewSession()
    {
        FlashcardReview = new FlashcardReview();
        await _sessionStorageService.SetItemAsync(Key, FlashcardReview);
    }
    
    public async Task UpdateReviewSession(FlashcardReview flashcardReview)
    {
        FlashcardReview = flashcardReview;
        await _sessionStorageService.SetItemAsync(Key, flashcardReview);
    }
}