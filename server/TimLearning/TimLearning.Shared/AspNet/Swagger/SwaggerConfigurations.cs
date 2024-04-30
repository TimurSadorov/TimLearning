using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using TimLearning.Shared.AspNet.Swagger.Filters.Validation;

namespace TimLearning.Shared.AspNet.Swagger;

public static class SwaggerConfigurations
{
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
