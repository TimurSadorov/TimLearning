namespace TimLearning.DockerManager.ApiClient.V1.Request;

public class V1ServiceContainerRequest : V1ContainerRequest
{
    public List<string>? HealthcheckTest { get; init; }
}
