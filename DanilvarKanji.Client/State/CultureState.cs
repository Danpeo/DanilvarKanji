using DanilvarKanji.Client.Localization;
using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Client.State;

public class CultureState
{
  private readonly ILocalizationService _localizationService;
  private bool _isInitialized;

  public CultureState(ILocalizationService localizationService)
  {
    _localizationService = localizationService;
  }

  public Culture Culture { get; private set; }

  public async Task Init()
  {
    if (!_isInitialized)
    {
      Culture = await _localizationService.GetCurrentCulture();
      _isInitialized = true;
    }
  }
}