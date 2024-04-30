using Docker.DotNet.Models;
using TimLearning.DockerManager.Api.Services.Docker.Client;
using TimLearning.DockerManager.Api.Services.Docker.Data;

namespace TimLearning.DockerManager.Api.Services.Docker.Services;

public class NetworkService : INetworkService
{
    private readonly IDockerClientFactory _dockerClientFactory;

    public NetworkService(IDockerClientFactory dockerClientFactory)
    {
        _dockerClientFactory = dockerClientFactory;
    }

    public async Task<string> Create(NetworkType type, CancellationToken ct = default)
    {
        using var client = _dockerClientFactory.Create();

        var name = Guid.NewGuid().ToString();
        var newNetwork = await client.Networks.CreateNetworkAsync(
            new NetworksCreateParameters
            {
                Name = name,
                // Internal = true,
                Driver = type.ToString().ToLower()
            },
            ct
        );

        return newNetwork.ID;
    }

    public async Task Delete(string idOrName, CancellationToken ct = default)
    {
        using var client = _dockerClientFactory.Create();

        await client.Networks.DeleteNetworkAsync(idOrName, ct);
    }
}
