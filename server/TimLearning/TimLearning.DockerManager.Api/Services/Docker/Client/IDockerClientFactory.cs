using Docker.DotNet;

namespace TimLearning.DockerManager.Api.Services.Docker.Client;

public interface IDockerClientFactory
{
    IDockerClient Create();
}
