using DanilvarKanji.Services.Auth;
using DanilvarKanji.Services.Characters;
using DanilvarKanji.Services.Images;
using DanilvarKanji.Services.Infrastructure;
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
            registry.For<ICharacterLearningManagementService>().Add<CharacterLearningManagementService>().Scoped();
            registry.For<IMemberService>().Add<MemberService>().Scoped();
            registry.For<ITokenService>().Add<TokenService>().Scoped();
            registry.For<ICharacterLearningService>().Add<CharacterLearningService>().Scoped();
            registry.For<IImageService>().Add<ImageService>().Scoped();
            registry.For<IUnitOfWork>().Add<UnitOfWork>().Scoped();
        });
    }
}