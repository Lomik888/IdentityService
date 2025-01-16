using IdentityService.Application.Extensions;
using IdentityService.Application.Mapping;
using IdentityService.Application.Services;
using IdentityService.Domain.Interfaces.Extensions;
using IdentityService.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.InitServices();
        services.InitExtensions();
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IIdentityService, Services.IdentityService>();
    }

    private static void InitExtensions(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
    }
}