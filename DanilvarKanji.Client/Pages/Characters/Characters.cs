using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class Characters
{
    private GetAllFromCharacterResponse? _selectedCharacter;
    
    private void HandleCharacterSelected(GetAllFromCharacterResponse getAllFromCharacter) 
        => _selectedCharacter = getAllFromCharacter;
}