using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class Characters
{
    private CharacterDto? _selectedCharacter;
    
    private void HandleTrailSelected(CharacterDto character) 
        => _selectedCharacter = character;
}