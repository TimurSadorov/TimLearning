using Microsoft.Extensions.DependencyInjection;

namespace TimLearning.Shared.Configuration.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRequiredOptions<TOptions>(this IServiceCollection services)
        where TOptions : class, IConfigurationSettings
    {
        return services.AddRequiredOptions<TOptions>(TOptions.SectionName);
    }

    public static IServiceCollection AddRequiredOptions<TOptions>(
        this IServiceCollection services,
        string sectionName
    )
        where TOptions : class
    {
        services
            .AddOptions<TOptions>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
