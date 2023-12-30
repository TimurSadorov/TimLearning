using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TimLearning.Shared.App.Auth.Configuration;
using TimLearning.Shared.Configuration.Extensions;

namespace TimLearning.Shared.App.Extension;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddTimLearningAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = configuration.GetRequiredSettings<JwtSecretSetting>();

        services.AddRequiredOptions<JwtSecretSetting>();

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
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSettings.KeyInByte),
                    ValidateIssuerSigningKey = true
                };
            });

        return services;
    }

    public static IApplicationBuilder UseTimLearningAuthentication(this IApplicationBuilder app)
    {
        return app.UseAuthentication();
    }
}
