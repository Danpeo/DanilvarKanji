using Blazored.Modal;
using Blazored.Modal.Services;
using Danilvar.Components.Pagination;
using DanilvarKanji.Client.Localization;
using DanilvarKanji.Client.Services;
using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Client.Shared.Modal;
using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Responses.Character;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class DisplayCharacters
{
    [CascadingParameter] public required IModalService ModalService { get; set; }
    [Inject] public ICharacterService CharacterService { get; set; } = default!;
    [Inject] public required ILocalizationService LocalizationService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Parameter, EditorRequired] public EventCallback<CharacterResponseResponseFull> OnSelected { get; set; }
    [Parameter] public int TakeQty { get; set; } = 2;
    [Parameter] public int PageNumber { get; set; } = 1;
    [Parameter] public int PageSize { get; set; } = 10;

    private List<CharacterResponseResponseFull>? _characterItems;
    private Culture _culture;
    private Dictionary<string, List<string>>? _kanjiMeanings = new();
    private string _searchTerm = string.Empty;

    protected override async Task OnInitializedAsync() =>
        await GetDataOfCharacters();

    private async Task GetDataOfCharacters()
    {
        await GetCurrentCulture();

        _characterItems =
            (List<CharacterResponseResponseFull>?)await CharacterService.ListCharactersAsync(PageNumber, PageSize);

        _kanjiMeanings = await CharacterService.SetKanjiMeanings(_characterItems, TakeQty, _culture);
    }

    private void DeleteCharacter(CharacterResponseResponseFull character)
    {
        var options = new ModalOptions
        {
            Position = ModalPosition.Middle,
            Class = "custom-modal"
        };

        var parameters = new ModalParameters()
            .Add(nameof(ConfirmCancelModal.Message), $"Delete '{character.Definition}'?")
            .Add(nameof(ConfirmCancelModal.OnConfirm), () =>
            {
                _characterItems?.Remove(character);
                StateHasChanged();
                CharacterService.DeleteCharacterAsync(character.Id);
            });

        ModalService.Show<ConfirmCancelModal>("Are you sure?", parameters, options);
    }

    private async Task UpdateCharacterItems()
    {
        IEnumerable<CharacterResponseResponseFull?>? characters =
            await CharacterService.ListCharactersAsync(PageNumber, PageSize);

        if (characters is not null)
            _characterItems?.AddRange(characters!);
    }

    private async Task GetCurrentCulture() =>
        _culture = await LocalizationService.GetCurrentCulture();

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
        _characterItems = (List<CharacterResponseResponseFull>?)await CharacterService.SearchCharacters(_searchTerm);
}