using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class Characters
{
    [Inject] public ICharacterService CharacterService { get; set; } = default!;

    public IEnumerable<CharacterDto?>? CharacterItems { get; set; }

    private CharacterDto? _selectedCharacter;

    protected override async Task OnInitializedAsync()
    {
        CharacterItems = await CharacterService.ListCharactersAsync();
    }

    private void HandleTrailSelected(CharacterDto character) 
        => _selectedCharacter = character;
}