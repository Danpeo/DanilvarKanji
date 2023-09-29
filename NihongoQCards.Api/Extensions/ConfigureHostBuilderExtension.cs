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
        });
    }
}