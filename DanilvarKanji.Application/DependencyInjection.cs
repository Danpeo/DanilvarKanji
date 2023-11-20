using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DanilvarKanji.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Application>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Application>());
        return services;
    }
}