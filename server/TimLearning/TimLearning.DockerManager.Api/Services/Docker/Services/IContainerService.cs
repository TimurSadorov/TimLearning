using TimLearning.DockerManager.Api.Services.Data;
using TimLearning.DockerManager.Api.Services.Docker.Data;

namespace TimLearning.DockerManager.Api.Services.Docker.Services;

public interface IContainerService
{
    Task<OperationResult<ContainerStartingResult>> CreateAndStart(
        ContainerDto containerData,
        CancellationToken ct = default
    );

    Task Delete(string idOrNameContainer, bool force, CancellationToken ct = default);

    Task<OperationResult> WaitHealthy(
        string containerId,
        TimeSpan retryTime,
        CancellationToken ct = default
    );

    Task<OperationResult> WaitForSuccessfulStop(
        string containerId,
        TimeSpan timout,
        CancellationToken ct = default
    );

    Task Prune(int beforeMin, CancellationToken ct = default);
}
