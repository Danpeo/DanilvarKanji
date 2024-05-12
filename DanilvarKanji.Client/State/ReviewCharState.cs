using Blazored.SessionStorage;
using DanilvarKanji.Shared.Responses.CharacterLearning;

namespace DanilvarKanji.Client.State;

public class ReviewCharState
{
  private const string NextInReviewKey = "NextInReviewChar";
  private readonly ISessionStorageService _sessionStorageService;

  private bool _isInitialized;

  public ReviewCharState(ISessionStorageService sessionStorageService)
  {
    _sessionStorageService = sessionStorageService;
  }

  public CharacterLearningResponseBase NextToReview { get; private set; } = new();

  public async Task Init()
  {
    if (!_isInitialized)
    {
      NextToReview = await _sessionStorageService.GetItemAsync<CharacterLearningResponseBase>(
        NextInReviewKey
      );
      _isInitialized = true;
    }
  }

  public async Task UpdateNextToReview(CharacterLearningResponseBase newToReview)
  {
    NextToReview = newToReview;
    await _sessionStorageService.SetItemAsync(NextInReviewKey, newToReview);
  }
}