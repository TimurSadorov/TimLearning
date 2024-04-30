using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TimLearning.Api.Consts;
using TimLearning.Api.Features.Controllers.User;
using TimLearning.Api.Filters;
using TimLearning.Domain.Configurations.Options;
using TimLearning.Shared.AspNet.Swagger;
using TimLearning.Shared.AspNet.Validations.Filters;
using TimLearning.Shared.Configuration.Extensions;

namespace TimLearning.Api.Configurations;

public static class ApiConfigurations
{
    public static IServiceCollection AddAllApiServices(
        this IServiceCollection services,
        IConfiguration configuration,
        bool useSwagger,
        string siteUrl
    )
    {
        services
            .AddControllers(options =>
            {
                options.Filters.Add<LocalizedValidationExceptionFilter>();
                options.Filters.Add<AccessExceptionFilter>();
                options.Filters.Add<NotFoundExceptionFilter>();
            })
            .AddApplicationPart(typeof(UserAccountController).Assembly);

        services.AddTimLearningAuthentication(configuration);
        services.AddTimLearningAuthorization();

        if (useSwagger)
        {
            services.AddTimLearningSwaggerGen();
        }

        services.AddCors(
            options =>
                options.AddPolicy(
                    CorsNames.TimLearningSite,
                    builder =>
                        builder
                            .WithOrigins(siteUrl)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .SetPreflightMaxAge(TimeSpan.FromDays(30))
                )
        );

        return services;
    }

    public static IServiceCollection AddTimLearningAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = configuration.GetRequiredConfig<JwtSecretOptions>();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSettings.KeyInByte)
                };
            });

        return services;
    }

    public static IApplicationBuilder UseTimLearningAuthentication(this IApplicationBuilder app)
    {
        return app.UseAuthentication();
    }

    public static IServiceCollection AddTimLearningAuthorization(this IServiceCollection services)
    {
        return services.AddAuthorization();
    }

    public static IApplicationBuilder UseTimLearningAuthorization(this IApplicationBuilder app)
    {
        return app.UseAuthorization();
    }
}
