using DanilvarKanji.Client.Mappers;
using DanilvarKanji.Client.Models.Responses;
using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Client.State;
using DanilvarKanji.Shared.Requests.Characters;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters.Manage;

public partial class AddCharacter
{
    private CharacterRequest _characterRequest = new();
    private string? _errorMessage;
    private bool _submitSuccessful;

    [Inject] public ICharacterService CharacterService { get; set; } = default!;

    [Inject] public required AppState AppState { get; set; }

    private GetKanjiResponse_KAD? _KAD_Kanji { get; set; }

    protected override void OnInitialized()
    {
        _KAD_Kanji = AppState.AddCharacterState.KanjiToAdd;

        if (_KAD_Kanji is not null) _characterRequest = _KAD_Kanji.ToCreateCharacterRequest();
    }

    private async Task HandleSubmit()
    {
        try
        {
            CharacterRequest? character = await CharacterService.AddCharacterAsync(_characterRequest);

            if (character is not null) _submitSuccessful = true;
        }
        catch (HttpRequestException e)
        {
            _errorMessage = e.Message;
        }
    }

    private void HandleInvalidSubmit()
    {
        _submitSuccessful = false;
    }
}