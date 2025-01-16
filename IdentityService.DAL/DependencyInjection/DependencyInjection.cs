using DotNetEnv;
using IdentityService.DAL.Repositories;
using IdentityService.DAL.Repositories.RedisRepositories;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories.AccessTokenRepository;
using IdentityService.Domain.Interfaces.Repositories.RefreshTokenRepository;
using IdentityService.Domain.Interfaces.Repositories.UserRepository;
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
        //services.AddScoped<IBaseRepository<TEntity>, BaseRepository<TEntity>>();
        services.AddScoped<IAccessTokenBlackListRedisRepository, AccessTokenBlackListRedisRepository>();
        services.AddScoped<IUserRedisRepository, UserRedisRepository>();
        services.AddScoped<IUserRepository<User>, UserRepository>();
        services.AddScoped<IRefreshTokenRepository<RefreshToken>, RefreshTokenRepository>();
    }
}