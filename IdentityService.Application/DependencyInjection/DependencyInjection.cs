using DotNetEnv;
using IdentityService.Application.Extantions;
using IdentityService.Application.Mapping;
using IdentityService.Application.Services;
using IdentityService.Domain.Interfaces.Extantions;
using IdentityService.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace IdentityService.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        // services.AddScoped<IDatabase>(cfg =>
        // {
        //     var multiplexer = ConnectionMultiplexer.Connect(Env.GetString("REDIS_URL"));
        //     return multiplexer.GetDatabase();
        // });
        //
        // services.AddStackExchangeRedisCache(options =>
        // {
        //     options.Configuration = Env.GetString("REDIS_URL_USERS");
        //     options.InstanceName = Env.GetString("REDIS_INSTANCENAME");
        // });

        services.InitServices();
        services.InitExtations();
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IIdentityService, Services.IdentityService>();
    }

    private static void InitExtations(this IServiceCollection services)
    {
        // services.AddScoped<IRedisService, RedisService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
    }
}