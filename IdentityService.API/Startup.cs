using System.Text;
using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.API.Middlewear;
using IdentityService.Application.DependencyInjection;
using IdentityService.DAL.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.API;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Env.GetString("JWT_ISSUER"),
                    ValidateAudience = true,
                    ValidAudience = Env.GetString("JWT_AUDIENCE"),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("JWT_SECRET_KEY"))),
                    ValidateLifetime = true,
                };
            });
        services.AddAuthorization();

        services.AddApplicationLayer();
        services.AddDataAccessLayer();

        services.AddExceptionHandler<ExceptionHandlerMiddlewear>();

        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        services.AddSwaggerGen();

        services.AddControllers();
    }
}