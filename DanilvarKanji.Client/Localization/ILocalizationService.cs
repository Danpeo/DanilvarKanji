using DanilvarKanji.Shared.Domain.Enumerations;

namespace DanilvarKanji.Client.Localization;

public interface ILocalizationService
{
    Task<Culture> GetCurrentCulture();
}