using Blazored.SessionStorage;
using DanilvarKanji.Shared.Responses.Exercise;

namespace DanilvarKanji.Client.State;

public class ReviewSessionState
{
    private const string LastReviewSessionKey = "LastReviewSession";

    private bool _isInitialized;
    private readonly ISessionStorageService _sessionStorageService;

    public ReviewSession ReviewSession { get; private set; } = new();

    public ReviewSessionState(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }
    
    public async Task Init()
    {
        if (!_isInitialized)
        {
            ReviewSession = await _sessionStorageService.GetItemAsync<ReviewSession>(LastReviewSessionKey);
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