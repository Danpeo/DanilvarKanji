using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DanilvarKanji.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");


        return services;
    }
}