using Blazored.SessionStorage;

namespace DanilvarKanji.Client.State;

public class ReviewSessionState
{
    private const string LastReviewSessionKey = "LastReviewSession";
    private readonly ISessionStorageService _sessionStorageService;

    private bool _isInitialized;

    public ReviewSessionState(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public ReviewSession ReviewSession { get; private set; } = new();

    public async Task Init()
    {
        if (!_isInitialized)
        {
            ReviewSession = await _sessionStorageService.GetItemAsync<ReviewSession>(
                LastReviewSessionKey
            );
            _isInitialized = true;
        }
    }

    public async Task NewReviewSession()
    {
        ReviewSession = new ReviewSession();
        await _sessionStorageService.SetItemAsync(LastReviewSessionKey, ReviewSession);
    }

    public async Task UpdateReviewSession(ReviewSession reviewSession)
    {
        ReviewSession = reviewSession;
        await _sessionStorageService.SetItemAsync(LastReviewSessionKey, reviewSession);
    }
}