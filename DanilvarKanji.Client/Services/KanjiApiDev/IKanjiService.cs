using DanilvarKanji.Client.Models.Responses;

namespace DanilvarKanji.Client.Services.KanjiApiDev;

public interface IKanjiService
{
  Task<GetKanjiResponse_KAD?> GetKanjiAsync(string kanji);
}