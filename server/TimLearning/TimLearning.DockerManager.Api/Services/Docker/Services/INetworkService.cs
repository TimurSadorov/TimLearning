using TimLearning.DockerManager.Api.Services.Docker.Data;

namespace TimLearning.DockerManager.Api.Services.Docker.Services;

public interface INetworkService
{
    Task<string> Create(NetworkType type, CancellationToken ct = default);

    Task Delete(string idOrName, CancellationToken ct = default);
}
