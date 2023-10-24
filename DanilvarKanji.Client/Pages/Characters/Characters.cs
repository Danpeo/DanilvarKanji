using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class Characters
{
    [Inject] public ICharacterService? CharacterService { get; set; }

    public IEnumerable<CharacterDto>? CharacterItems { get; set; }

    protected override async Task OnInitializedAsync()
    {
        CharacterItems = await CharacterService.ListCharactersAsync();
    }
}