using Danilvar.Components.JSWrapper;
using Microsoft.Extensions.DependencyInjection;

namespace Danilvar.Components;

public static class DependencyInjection
{
    public static IServiceCollection AddComponents(this IServiceCollection services)
    {
        services.AddSingleton<JSDomFunctions>();  

        return services;
    }
}