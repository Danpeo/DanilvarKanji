using Blazored.LocalStorage;
using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Client.Localization;

public class LocalizationService : ILocalizationService
{
    private readonly ILocalStorageService _localStorageService;
    
    public LocalizationService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
    
    public async Task<Culture> GetCurrentCulture()
    {
        Culture culture = await _localStorageService.GetItemAsync<string>("culture") switch
        {
            "en-US" => Culture.EnUS,
            "ru-RU" => Culture.RuRU,
            _ => Culture.EnUS
        };

        return culture;
    }
}