using DanilvarKanji.Data.Configuration;
using DanilvarKanji.Domain.Settings;
using DanilvarKanji.Shared.Domain.Settings;

namespace DanilvarKanji.OptionsSetup;

public static class ApplicationSettingsSetup
{
    public static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<CharacterLearningSettings>(config
            .GetSection("CharacterLearningSettings"));
        
        services.Configure<CloudinarySettings>(config
            .GetSection("CloudinarySettings"));

        services.Configure<TesseractSettings>(config
            .GetSection("TesseractSettings"));
        
        return services;
    }
}