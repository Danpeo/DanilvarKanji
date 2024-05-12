using DanilvarKanji.Domain.Settings;
using DanilvarKanji.Domain.Shared.Params;

namespace DanilvarKanji.Setup;

public static class ApplicationSettingsSetup
{
  public static IServiceCollection AddApplicationSettings(
    this IServiceCollection services,
    IConfiguration config
  )
  {
    services.Configure<CharacterLearningSettings>(config.GetSection("CharacterLearningSettings"));

    services.Configure<TesseractSettings>(config.GetSection("TesseractSettings"));

    services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

    return services;
  }
}