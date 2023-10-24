using DanilvarKanji.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Characters;

public partial class DisplayCharacters
{
    [Parameter] 
    [EditorRequired] 
    public IEnumerable<CharacterDto> CharacterItems { get; set; } = default!;

    private List<string> GetKanjiMeaningsOrderedByPrioiry(CharacterDto character, int takeQty = 2) =>
        character.KanjiMeanings
            .OrderBy(x => x.Priority)
            .Take(takeQty)
            .Select(x => x.Definition)
            .ToList();
    
    
}