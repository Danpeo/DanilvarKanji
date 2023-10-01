using DanilvarKanji.Services.Auth;
using DanilvarKanji.Services.Characters;
using Lamar;
using Lamar.Microsoft.DependencyInjection;

namespace DanilvarKanji.Extensions;

public static class ConfigureHostBuilderExtension
{
    public static void AddLamarServices(this ConfigureHostBuilder host)
    {
        host.UseLamar((context, registry) =>
        {
            registry.For<ICharacterService>().Add<CharacterService>().Scoped();
            registry.For<ICharacterLearningService>().Add<CharacterLearningService>().Scoped();
            registry.For<IUserService>().Add<UserService>().Scoped();
            registry.For<ITokenService>().Add<TokenService>().Scoped();
        });
    }
}