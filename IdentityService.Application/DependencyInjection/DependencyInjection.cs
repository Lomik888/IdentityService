using IdentityService.Application.Extantions;
using IdentityService.Application.Mapping;
using IdentityService.Domain.Interfaces.Extantions;
using IdentityService.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        
        services.InitServices();
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, Services.IdentityService>();
    }
}