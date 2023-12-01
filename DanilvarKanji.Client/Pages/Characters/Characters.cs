using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Shared.Responses.Character;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class Characters
{
    private CharacterResponse? _selectedCharacter;
    
    private void HandleTrailSelected(CharacterResponse character) 
        => _selectedCharacter = character;
}