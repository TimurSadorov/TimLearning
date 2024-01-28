using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TimLearning.Api.Configurations.Options;
using TimLearning.Api.Features.Controllers.User;
using TimLearning.Shared.Configuration.Extensions;
using TimLearning.Shared.Validation.AspNet.Filters;

namespace TimLearning.Api.Configurations;

public static class ApiConfigurations
{
    public static IServiceCollection AddAllApiServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddControllers(options => options.Filters.Add<LocalizedValidationExceptionFilter>())
            .AddApplicationPart(typeof(UserController).Assembly);

        services.AddTimLearningAuthentication(configuration);
        services.AddTimLearningAuthorization();

        services.AddTimLearningSwaggerGen();

        return services;
    }

    public static IServiceCollection AddTimLearningAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = configuration.GetRequiredConfig<JwtSecretOptions>();

        services.AddRequiredOptions<JwtSecretOptions>();

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
                    ValidateIssuer = true,
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

    public static IServiceCollection AddTimLearningSwaggerGen(
        this IServiceCollection services,
        Action<SwaggerGenOptions>? setupAction = null
    )
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                }
            );
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );

            setupAction?.Invoke(options);
        });

        return services;
    }

    // ReSharper disable once InconsistentNaming
    public static IApplicationBuilder UseTimLearningSwaggerAndUI(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
