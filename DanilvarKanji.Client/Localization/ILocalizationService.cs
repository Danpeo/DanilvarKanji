using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Client.Localization;

public interface ILocalizationService
{
    Task<Culture> GetCurrentCulture();
}