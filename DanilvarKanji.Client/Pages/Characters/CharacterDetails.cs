using DanilvarKanji.Client.Localization;
using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.DTO;
using DanilvarKanji.Shared.Entities.Enums;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class CharacterDetails
{
    [Inject] public ICharacterService CharacterService { get; set; } = default!;
    [Inject] public ILocalizationService? LocalizationService { get; set; }
    [Parameter, EditorRequired] public CharacterDto? Character { get; set; }

    [Parameter] public int TakeQty { get; set; } = 2;

    private Dictionary<string, List<string>> _kanjiMeanings = new();
    private Culture _culture = Culture.EnUS;
    private bool _isOpen;
    private CharacterDto? _activeCharacter;

    protected override async Task OnInitializedAsync()
    {
        await GetCurrentCulture();
    }
    
    protected async override Task OnParametersSetAsync()
    {
        if (Character != null)
        {
            _activeCharacter = Character;
            _isOpen = true;
            
            _kanjiMeanings[_activeCharacter.Id] =
                await CharacterService.GetCharacterKanjiMeanings(_activeCharacter.Id, TakeQty, _culture);
        }
    }

    private async Task GetCurrentCulture()
    {
        _culture = await LocalizationService.GetCurrentCulture();
    }

    private void Close()
    {
        _activeCharacter = null;
        _isOpen = false;
    }
}