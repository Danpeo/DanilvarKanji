using Blazored.SessionStorage;
using DanilvarKanji.Shared.Responses.CharacterLearning;

namespace DanilvarKanji.Client.State;

public class ReviewCharState
{
    private const string NextInReviewKey = "NextInReviewChar";

    private bool _isInitialized;
    private readonly ISessionStorageService _sessionStorageService;

    public GetCharacterLearningBaseInfoResponse NextToReview { get; private set; } = new();

    public ReviewCharState(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public async Task Init()
    {
        if (!_isInitialized)
        {
            NextToReview =
                await _sessionStorageService.GetItemAsync<GetCharacterLearningBaseInfoResponse>(NextInReviewKey);
            _isInitialized = true;
        }
    }

    public async Task UpdateNextToReview(GetCharacterLearningBaseInfoResponse newToReview)
    {
        NextToReview = newToReview;
        await _sessionStorageService.SetItemAsync(NextInReviewKey, newToReview);
    }
}