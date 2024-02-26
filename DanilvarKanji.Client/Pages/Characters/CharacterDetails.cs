using DanilvarKanji.Client.Localization;
using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Responses.Character;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class CharacterDetails
{
    [Parameter, EditorRequired] public CharacterResponseResponseFull? Character { get; set; }
    [Parameter] public int TakeQty { get; set; } = 2;
    [Inject] public ICharacterService CharacterService { get; set; } = default!;


    private Dictionary<string, List<string>> _kanjiMeanings = new();
    private bool _isOpen;
    private CharacterResponseResponseFull? _activeCharacter;
    
    protected override async Task OnParametersSetAsync()
    {
        if (Character != null)
        {
            _activeCharacter = Character;
            _isOpen = true;
            
            _kanjiMeanings[_activeCharacter.Id] =
                await CharacterService.GetCharacterKanjiMeanings(_activeCharacter.Id, TakeQty, Culture);
        }
    }

    private void Close()
    {
        _activeCharacter = null;
        _isOpen = false;
    }
}