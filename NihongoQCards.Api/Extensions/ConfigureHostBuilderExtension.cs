using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Services.Images;
using Lamar;
using Lamar.Microsoft.DependencyInjection;

namespace DanilvarKanji.Extensions;

public static class ConfigureHostBuilderExtension
{
    [Obsolete("Should be disposed in time!")]
    public static void AddLamarServices(this ConfigureHostBuilder host)
    {
        host.UseLamar((context, registry) =>
        {
           // registry.For<ICharacterLearningManagementService>().Add<CharacterLearningManagementService>().Scoped();
           // registry.For<ICharacterLearningService>().Add<CharacterLearningService>().Scoped();
            registry.For<IImageService>().Add<ImageService>().Scoped();
        });
    }
}