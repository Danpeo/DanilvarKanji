using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Auth;
using DanilvarKanji.Infrastructure.Common;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DanilvarKanji.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresSql")));

        services.AddTransient<IDateTime, MachineDateTime>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICharacterRepository, CharacterRepository>();
        services.AddScoped<ICharacterLearningRepository, CharacterLearningRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}