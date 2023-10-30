using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters.Manage;

public partial class AddCharacter
{
    [Inject] public ICharacterService CharacterService { get; set; } = default!;

    private CharacterDto _character = new();

    private async Task HandleSubmit()
    {
        HttpResponseMessage response = await CharacterService.AddCharacterAsync(_character);
    }
}