using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Client.Localization;

public interface ILocalizationService
{
    Task<Culture> GetCurrentCulture();
}