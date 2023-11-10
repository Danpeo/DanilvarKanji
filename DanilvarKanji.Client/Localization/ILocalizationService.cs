using DanilvarKanji.Shared.Entities.Enums;

namespace DanilvarKanji.Client.Localization;

public interface ILocalizationService
{
    Task<Culture> GetCurrentCulture();
}