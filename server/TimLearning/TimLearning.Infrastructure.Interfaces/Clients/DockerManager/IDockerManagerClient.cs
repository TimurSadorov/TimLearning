using TimLearning.DockerManager.ApiClient.V1.Response;
using TimLearning.Domain.Entities.Data;

namespace TimLearning.Infrastructure.Interfaces.Clients.DockerManager;

public interface IDockerManagerClient
{
    Task<V1AppTestResponse> TestApp(
        FileInfo appArchive,
        MainAppContainerData mainAppContainerData,
        string relativePathToDockerfile,
        IEnumerable<ServiceContainerImageData> serviceApps,
        CancellationToken ct = default
    );
}
