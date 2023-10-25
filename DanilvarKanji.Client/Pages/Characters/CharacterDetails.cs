using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class CharacterDetails
{
    [Parameter][EditorRequired]
    public string? Id { get; set; }

    [Inject] public ICharacterService CharacterService { get; set; } = default!;

    public CharacterDto? Character { get; set; }


    protected override async Task OnInitializedAsync()
    {
        Character = await CharacterService.GetCharacterAsync(Id);
    }
}