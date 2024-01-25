using DanilvarKanji.Client.Mappers;
using DanilvarKanji.Client.Models.Responses;
using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Client.State;
using DanilvarKanji.Shared.Requests.Characters;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters.Manage;

public partial class AddCharacter
{
    [Inject] public ICharacterService CharacterService { get; set; } = default!;

    [Inject] public required AppState AppState { get; set; }

    private GetKanjiResponse_KAD? _KAD_Kanji { get; set; }
    private CreateCharacterRequest _createCharacterRequest = new();
    private bool _submitSuccessful;
    private string? _errorMessage;

    protected override void OnInitialized()
    {
        _KAD_Kanji = AppState.AddCharacterState.KanjiToAdd;

        if (_KAD_Kanji is not null)
        {
            _createCharacterRequest = _KAD_Kanji.ToCreateCharacterRequest();
        }
    }

    private async Task HandleSubmit()
    {
        CreateCharacterRequest? character = await CharacterService.AddCharacterAsync(_createCharacterRequest);

        if (character is not null)
        {
            _submitSuccessful = true;
        }
    }

    private void HandleInvalidSubmit()
    {
        _submitSuccessful = false;
        _errorMessage = "There was a problem";
    }
}