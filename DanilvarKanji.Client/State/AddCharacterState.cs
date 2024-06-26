using Blazored.SessionStorage;
using DanilvarKanji.Client.Models.Responses;

namespace DanilvarKanji.Client.State;

public class AddCharacterState
{
    private const string NewCharacterKey = "NewCharacter";
    private readonly ISessionStorageService _sessionStorageService;

    private bool _isInitialized;

    public AddCharacterState(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public GetKanjiResponse_KAD KanjiToAdd { get; set; } = new();

    public async Task Init()
    {
        if (!_isInitialized)
        {
            KanjiToAdd = await _sessionStorageService.GetItemAsync<GetKanjiResponse_KAD>(NewCharacterKey);
            _isInitialized = true;
        }
    }

    public async Task UpdateNextToReview(GetKanjiResponse_KAD kanjiToAdd)
    {
        KanjiToAdd = kanjiToAdd;
        await _sessionStorageService.SetItemAsync(NewCharacterKey, kanjiToAdd);
    }
}