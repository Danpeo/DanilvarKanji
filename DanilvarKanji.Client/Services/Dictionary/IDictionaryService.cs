using DanilvarKanji.Client.Models.Responses.JMdict;

namespace DanilvarKanji.Client.Services.Dictionary;

public interface IDictionaryService
{
  Task<IEnumerable<Word?>?> ListWordsByKanaAsync(string entry, bool useRomaji = false);
  Task<IEnumerable<Word?>?> ListWordsByKanjiAsync(string entry);
  Task<IEnumerable<Word?>?> ListWordsByTranslationAsync(string entry);
}