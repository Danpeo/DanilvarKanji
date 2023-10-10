using DanilvarKanji.Data.Configuration;

namespace DanilvarKanji.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddCors();
        services.Configure<CharacterLearningSettings>(config
            .GetSection("CharacterLearningSettings"));

        return services;
    }
}