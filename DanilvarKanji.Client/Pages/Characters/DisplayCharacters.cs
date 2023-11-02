using DanilvarKanji.Client.Localization;
using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.DTO;
using DanilvarKanji.Shared.Models;
using DanilvarKanji.Shared.Models.Enums;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class DisplayCharacters
{
    [Inject] public ICharacterService? CharacterService { get; set; }
    [Inject] public ILocalizationService? LocalizationService { get; set; }
    [Parameter, EditorRequired]  public IEnumerable<CharacterDto> CharacterItems { get; set; } = default!;
    [Parameter, EditorRequired] public EventCallback<CharacterDto> OnSelected { get; set; }
    [Parameter] public int TakeQty { get; set; } = 2;

    private Culture _culture = Culture.EnUS;
    private Dictionary<string, List<string>>? _kanjiMeanings = new();

    protected override async Task OnInitializedAsync()
    {
        await GetCurrentCulture();
        _kanjiMeanings = await CharacterService!.SetKanjiMeanings(CharacterItems, TakeQty, _culture);
    }

    private async Task GetCurrentCulture()
    {
        _culture = await LocalizationService!.GetCurrentCulture();
    }
}