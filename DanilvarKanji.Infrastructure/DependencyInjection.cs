using DanilvarKanji.Domain.Persistance;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Auth;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Infrastructure.Emails;
using DanilvarKanji.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DanilvarKanji.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresSql"))
        );

        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICharacterRepository, CharacterRepository>();
        services.AddScoped<ICharacterLearningRepository, CharacterLearningRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IFlashcardRepository, FlashcardRepository>();
        
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        return services;
    }
}