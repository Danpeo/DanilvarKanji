using DanilvarKanji.Shared.Responses.Character;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class Characters
{
    [Inject] public IStringLocalizer<App> Loc { get; set; } = null!;
    
    private CharacterResponseResponseFull? _selectedCharacter;
    
    private void HandleCharacterSelected(CharacterResponseResponseFull character)
    {
        _selectedCharacter = character;
    }
}