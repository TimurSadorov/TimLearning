using TimLearning.DockerManager.ApiClient;
using TimLearning.DockerManager.ApiClient.V1.Dto;
using TimLearning.DockerManager.ApiClient.V1.Request;
using TimLearning.DockerManager.ApiClient.V1.Response;
using TimLearning.Domain.Entities.Data;
using TimLearning.Infrastructure.Implementation.Clients.DockerManager.Mappers;
using TimLearning.Infrastructure.Interfaces.Clients.DockerManager;

namespace TimLearning.Infrastructure.Implementation.Clients.DockerManager;

public class DockerManagerClient : IDockerManagerClient
{
    private readonly IDockerManagerApiClientFactory _dockerManagerApiClientFactory;

    public DockerManagerClient(IDockerManagerApiClientFactory dockerManagerApiClientFactory)
    {
        _dockerManagerApiClientFactory = dockerManagerApiClientFactory;
    }

    public Task<V1AppTestResponse> TestApp(
        FileInfo appArchive,
        MainAppContainerData mainAppContainerData,
        string relativePathToDockerfile,
        IEnumerable<ServiceContainerImageData> serviceApps,
        CancellationToken ct = default
    )
    {
        var client = _dockerManagerApiClientFactory.CreateV1Client();

        var dto = new V1TestAppDto(
            appArchive,
            new V1MainAppContainerRequest
            {
                Hostname = mainAppContainerData.Hostname,
                Envs = mainAppContainerData.Envs?.Select(e => e.ToRequest()).ToList()
            },
            relativePathToDockerfile,
            serviceApps
                .Select(
                    a =>
                        new V1ServiceContainerImageRequest
                        {
                            Name = a.Name,
                            Tag = a.Tag,
                            Container = new V1ServiceContainerRequest
                            {
                                Hostname = a.ContainerData.Hostname,
                                Envs = a.ContainerData.Envs?.Select(e => e.ToRequest()).ToList(),
                                HealthcheckTest = a.ContainerData.HealthcheckTest
                            }
                        }
                )
                .ToList()
        );

        return client.TestApp(dto, ct);
    }
}
