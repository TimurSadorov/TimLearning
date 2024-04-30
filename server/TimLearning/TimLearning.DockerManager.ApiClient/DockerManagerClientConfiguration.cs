using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TimLearning.Shared.Configuration.Extensions;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace TimLearning.DockerManager.ApiClient;

public static class DockerManagerClientConfiguration
{
    public static IServiceCollection AddDockerManagerClient(this IServiceCollection services)
    {
        services.AddRequiredOptions<DockerManagerApiOptions>();

        services.TryAddSingleton<IDockerManagerApiClientFactory, DockerManagerApiClientFactory>();
        services
            .AddHttpClient(DockerManagerApiClientFactory.ClientName)
            .AddTransientHttpErrorPolicy(
                policyBuilder =>
                    policyBuilder.WaitAndRetryAsync(
                        Backoff.DecorrelatedJitterBackoffV2(
                            medianFirstRetryDelay: TimeSpan.FromSeconds(1),
                            retryCount: 2
                        )
                    )
            );

        return services;
    }
}
