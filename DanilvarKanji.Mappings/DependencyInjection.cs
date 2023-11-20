using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DanilvarKanji.Mappings;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var mappers = new Profile[]
        {
            new CharacterMapperProfile(),
            new UserMapperProfile()
        };
        

        services.AddAutoMapper(c => c.AddProfiles(mappers));
        
        return services;
    }
}