using Microsoft.Extensions.DependencyInjection;

namespace TimLearning.Shared.BackgroundTask;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackgroundPeriodicalTask<TTask>(this IServiceCollection services)
        where TTask : BackgroundPeriodicalTask<TTask>
    {
        return services
            .AddHostedService<TTask>()
            .AddLogging();
    }
}