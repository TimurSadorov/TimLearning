using TimLearning.DockerManager.Api.Services.Docker.Client;
using TimLearning.DockerManager.Api.Services.Docker.Services;
using TimLearning.Shared.Configuration.Extensions;

namespace TimLearning.DockerManager.Api.Services.Docker;

public static class DockerConfigurations
{
    public static IServiceCollection AddDocker(this IServiceCollection services)
    {
        services.AddRequiredOptions<DockerOptions>();
        services.AddSingleton<IDockerClientFactory, DockerClientFactory>();

        services.AddSingleton<INetworkService, NetworkService>();
        services.AddSingleton<IContainerService, ContainerService>();
        services.AddSingleton<IImageService, ImageService>();

        return services;
    }
}
