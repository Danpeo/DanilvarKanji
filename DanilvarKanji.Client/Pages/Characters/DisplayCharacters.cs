using Danilvar.Components.Pagination;
using DanilvarKanji.Client.Localization;
using DanilvarKanji.Client.Services;
using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Shared.Responses.Character;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class DisplayCharacters
{
    [Inject] public ICharacterService CharacterService { get; set; } = default!;
    [Inject] public ILocalizationService? LocalizationService { get; set; }
    [Parameter, EditorRequired] public EventCallback<CharacterResponse> OnSelected { get; set; }
    [Parameter] public int TakeQty { get; set; } = 2;
    [Parameter] public int PageNumber { get; set; } = 1;
    [Parameter] public int PageSize { get; set; } = 10;
    
    private List<CharacterResponse>? _characterItems;
    private Culture _culture = Culture.EnUS;
    private Dictionary<string, List<string>>? _kanjiMeanings = new();
    private string _searchTerm = string.Empty;

    protected override async Task OnInitializedAsync() => 
        await GetDataOfCharacters();

    private async Task GetDataOfCharacters()
    {
        await GetCurrentCulture();

        _characterItems = (List<CharacterResponse>?)await CharacterService.ListCharactersAsync(PageNumber, PageSize);

        _kanjiMeanings = await CharacterService.SetKanjiMeanings(_characterItems, TakeQty, _culture);
    }

    private async Task UpdateCharacterItems()
    {
        
        IEnumerable<CharacterResponse?>? characters = await CharacterService.ListCharactersAsync(PageNumber, PageSize);

        if (characters is not null)
            _characterItems?.AddRange(characters!);
    }

    private async Task GetCurrentCulture() =>
        _culture = await LocalizationService!.GetCurrentCulture();

    private async Task SearchForCharacterKey(KeyboardEventArgs args)
    {
        if (args.Key != "Enter")
            return;

        await SearchForCharacter();
    }

    private async Task OnUpdateContentClick()
    {
        PageNumber++;
        await UpdateCharacterItems();
    }

    private async Task SearchForCharacter() =>
        _characterItems = (List<CharacterResponse>?)await CharacterService.SearchCharacters(_searchTerm);

}