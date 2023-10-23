using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public class CharactersBase : ComponentBase
{
    [Inject] public ICharacterService? CharacterService { get; set; }

    public IEnumerable<CharacterDto> Characters { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Characters = await CharacterService.ListCharactersAsync();
    }
}