using Blazored.SessionStorage;

namespace DanilvarKanji.Client.State;

public class FlashcardReviewState : StateBase<FlashcardReview>
{
  private const string Key = "FlashcardReview";
  private readonly ISessionStorageService _sessionStorageService;

  public FlashcardReviewState(ISessionStorageService sessionStorageService)
  {
    _sessionStorageService = sessionStorageService;
  }

  public FlashcardReview FlashcardReview { get; private set; } = new();

  public override async Task InitAsync()
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