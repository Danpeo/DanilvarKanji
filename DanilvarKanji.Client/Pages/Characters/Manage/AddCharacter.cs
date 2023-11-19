using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Shared.Requests.Characters;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters.Manage;

public partial class AddCharacter
{
    [Inject] public ICharacterService CharacterService { get; set; } = default!;

    private CreateCharacterRequest _character = new();
    private bool _submitSuccessful;
    private string? _errorMessage;

    private async Task HandleSubmit()
    {
        CreateCharacterRequest? character = await CharacterService.AddCharacterAsync(_character);

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