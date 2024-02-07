using DanilvarKanji.Data.Configuration;
using CharacterLearningSettings = DanilvarKanji.Shared.Domain.Settings.CharacterLearningSettings;

namespace DanilvarKanji.OptionsSetup;

public static class ApplicationSettingsSetup
{
    public static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<CharacterLearningSettings>(config
            .GetSection("CharacterLearningSettings"));
        
        services.Configure<CloudinarySettings>(config
            .GetSection("CloudinarySettings"));


        return services;
    }
}