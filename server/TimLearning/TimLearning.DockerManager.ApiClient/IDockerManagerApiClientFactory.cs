using TimLearning.DockerManager.ApiClient.V1;

namespace TimLearning.DockerManager.ApiClient;

public interface IDockerManagerApiClientFactory
{
    public V1DockerManagerApiClient CreateV1Client();
}
