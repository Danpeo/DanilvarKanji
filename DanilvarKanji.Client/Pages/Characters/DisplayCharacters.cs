using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.DTO;
using DanilvarKanji.Shared.Models;
using DanilvarKanji.Shared.Models.Enums;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class DisplayCharacters
{
    [Inject] public ICharacterService? CharacterService { get; set; }
    [Parameter] [EditorRequired] public IEnumerable<CharacterDto> CharacterItems { get; set; } = default!;
    [Parameter] public int TakeQty { get; set; } = 2;

    private Culture _culture = Culture.EnUS;
    private List<string>? _kanjiMeanings = new();
    private Dictionary<string, List<string>>? _allKanjiMeanings = new();

    protected override async Task OnInitializedAsync()
    {
        await GetCurrentCulture();

        await SetKanjiMeanings();
    }

    private async Task GetCurrentCulture()
    {
        _culture = await LocalStorage.GetItemAsync<string>("culture") switch
        {
            "en-US" => Culture.EnUS,
            "ru-RU" => Culture.RuRU,
            _ => _culture
        };
    }

    private async Task SetKanjiMeanings()
    {
        foreach (CharacterDto characterItem in CharacterItems)
        {
            _kanjiMeanings = await CharacterService!.GetCharacterKanjiMeanings(characterItem.Id, TakeQty, _culture);

            if (_kanjiMeanings != null)
                _allKanjiMeanings![characterItem.Id] = _kanjiMeanings;
        }
    }

    private string GetCharacterDefinitionByCulture(CharacterDto character) =>
        character.Definitions
            .FirstOrDefault(x => x.Culture == _culture)
            ?.Value;
}