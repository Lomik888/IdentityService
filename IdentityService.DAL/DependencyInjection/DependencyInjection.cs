using DotNetEnv;
using IdentityService.DAL.Repositories;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddSingleton<DapperDbContext>();
        services.AddSingleton<RedisContext>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(Env.GetString("POSTGRESQL_CONNECTIONSTRING"),
                p => p.MigrationsAssembly("IdentityService.API"));
        });

        services.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRedisRepository, UserRedisRepository>();
        services.AddScoped<IUserRepository<User>, UserRepository>();
        services.AddScoped<IRefreshTokenRepository<RefreshToken>, RefreshTokenRepository>();
    }
}