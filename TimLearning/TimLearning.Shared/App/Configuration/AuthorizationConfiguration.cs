using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TimLearning.Shared.App.Configuration;

public static class AuthorizationConfiguration
{
    public static IServiceCollection AddTimLearningAuthorization(this IServiceCollection services)
    {
        return services.AddAuthorization();
    }

    public static IApplicationBuilder UseTimLearningAuthorization(this IApplicationBuilder app)
    {
        return app.UseAuthorization();
    }
}
