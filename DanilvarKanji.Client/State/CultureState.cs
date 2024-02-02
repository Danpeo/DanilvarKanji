using DanilvarKanji.Client.Localization;
using DanilvarKanji.Shared.Domain.Enumerations;

namespace DanilvarKanji.Client.State;

public class CultureState
{
    public Culture Culture { get; private set; }
    private readonly ILocalizationService _localizationService;
    private bool _isInitialized;

    public CultureState(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public async Task Init()
    {
        if (!_isInitialized)
        {
            Culture = await _localizationService.GetCurrentCulture();
            _isInitialized = true;
        }
    }
}