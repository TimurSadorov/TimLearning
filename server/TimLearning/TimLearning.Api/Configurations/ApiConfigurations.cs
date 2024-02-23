using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using TimLearning.Api.Consts;
using TimLearning.Api.Features.Controllers.User;
using TimLearning.Application.Configurations.Options;
using TimLearning.Shared.AspNet.Swagger.Filters.Validation;
using TimLearning.Shared.AspNet.Validations.Filters;
using TimLearning.Shared.Configuration.Extensions;

namespace TimLearning.Api.Configurations;

public static class ApiConfigurations
{
    public static IServiceCollection AddAllApiServices(
        this IServiceCollection services,
        IConfiguration configuration,
        string siteUrl
    )
    {
        services
            .AddControllers(options => options.Filters.Add<LocalizedValidationExceptionFilter>())
            .AddApplicationPart(typeof(UserAccountController).Assembly);

        services.AddTimLearningAuthentication(configuration);
        services.AddTimLearningAuthorization();

        services.AddTimLearningSwaggerGen();

        services.AddCors(
            options =>
                options.AddPolicy(
                    CorsNamesConsts.TimLearningSite,
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

    public static IServiceCollection AddTimLearningSwaggerGen(
        this IServiceCollection services,
        Action<SwaggerGenOptions>? setupAction = null
    )
    {
        services.AddSwaggerGen(options =>
        {
            options.AddServer(new OpenApiServer { Description = "Main server", Url = "/" });
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Server API", Version = "1.0.0" });

            options.CustomOperationIds(apiDesc =>
            {
                var methodName = apiDesc.TryGetMethodInfo(out var methodInfo)
                    ? methodInfo.Name
                    : throw new InvalidOperationException("Can not resolve OperationId for OAS.");

                return methodName;
            });

            const string securityShameName = "BearerToken";
            options.AddSecurityDefinition(
                securityShameName,
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                }
            );
            options.OperationFilter<SecurityRequirementsOperationFilter>(true, securityShameName);

            options.ConfigureValidationErrorOperation();

            setupAction?.Invoke(options);
        });

        return services;
    }

    // ReSharper disable once InconsistentNaming
    public static IApplicationBuilder UseTimLearningSwaggerAndUI(this WebApplication app)
    {
        app.UseSwagger(o =>
        {
            o.RouteTemplate = "/api-docs/{documentName}/swagger.json";
        });
        app.UseSwaggerUI(o =>
        {
            o.RoutePrefix = "api-docs";
            o.SwaggerEndpoint("/api-docs/v1/swagger.json", "Server API");

            o.DisplayOperationId();
            o.EnableFilter();
            o.EnablePersistAuthorization();
            o.ShowCommonExtensions();
            o.DefaultModelsExpandDepth(10);
        });

        return app;
    }
}
