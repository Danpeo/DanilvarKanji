using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.ValueObjects;
using WanaKanaNet;

namespace DanilvarKanji.Shared.Validation;

public class CharacterValidatorBase<T> : ValidatorBase<T>
{
  protected static bool HaveMeaningsInAllCultures(ICollection<KanjiMeaning>? kanjiMeanings)
  {
    if (kanjiMeanings == null || kanjiMeanings.Count < 2)
      return false;

    return kanjiMeanings.Any(meaning =>
             meaning.Definitions!.Any(def => def.Culture == Culture.EnUS)
           )
           && kanjiMeanings.Any(meaning => meaning.Definitions!.Any(def => def.Culture == Culture.RuRU));
  }

  protected static bool HaveStringDefinitionInAllCultures(ICollection<StringDefinition>? defs)
  {
    if (defs == null || defs.Count < 2)
      return false;

    return defs.Any(d => d.Culture == Culture.EnUS) && defs.Any(d => d.Culture == Culture.RuRU);
  }

  protected static bool StrIsJapanese(string? def)
  {
    return def != null && WanaKana.IsJapanese(def);
  }
}