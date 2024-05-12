using Microsoft.Extensions.DependencyInjection;

namespace DanilvarKanji.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    /*
          services.AddValidatorsFromAssemblyContaining<Application>();*/
    /*services.AddFluentValidationAutoValidation();
    services.AddFluentValidationClientsideAdapters();*/

    /*services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBeh<,>));*/

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Application>());
    return services;
  }
}