using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Shared.Responses.Character;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class CharacterDetails
{
  private CharacterResponseResponseFull? _activeCharacter;
  private bool _isOpen;

  private Dictionary<string, List<string>> _kanjiMeanings = new();

  [Parameter] [EditorRequired] public CharacterResponseResponseFull? Character { get; set; }

  [Parameter] public int TakeQty { get; set; } = 2;

  [Inject] public ICharacterService CharacterService { get; set; } = default!;

  protected override async Task OnParametersSetAsync()
  {
    if (Character != null)
    {
      _activeCharacter = Character;
      _isOpen = true;

      _kanjiMeanings[_activeCharacter.Id] = await CharacterService.GetCharacterKanjiMeanings(
        _activeCharacter.Id,
        TakeQty,
        Culture
      );
    }
  }

  private void Close()
  {
    _activeCharacter = null;
    _isOpen = false;
  }
}