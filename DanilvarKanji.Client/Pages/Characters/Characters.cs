using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class Characters
{
    private CharacterResponseResponseFull? _selectedCharacter;

    private void HandleCharacterSelected(CharacterResponseResponseFull character)
    {
        _selectedCharacter = character;
    }
}