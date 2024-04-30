namespace TimLearning.DockerManager.ApiClient.V1.Request;

public class V1ContainerRequest
{
    public string? Hostname { get; init; }
    public IReadOnlyList<V1ContainerEnvRequest>? Envs { get; init; }
}
