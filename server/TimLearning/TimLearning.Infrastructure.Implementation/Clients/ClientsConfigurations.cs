using Microsoft.Extensions.DependencyInjection;
using TimLearning.DockerManager.ApiClient;
using TimLearning.Infrastructure.Implementation.Clients.DockerManager;
using TimLearning.Infrastructure.Interfaces.Clients.DockerManager;

namespace TimLearning.Infrastructure.Implementation.Clients;

public static class ClientsConfigurations
{
    public static IServiceCollection AddAllClients(this IServiceCollection services)
    {
        services.AddDockerManagerClient();
        services.AddSingleton<IDockerManagerClient, DockerManagerClient>();

        return services;
    }
}
