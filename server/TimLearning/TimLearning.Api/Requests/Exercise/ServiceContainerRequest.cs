namespace TimLearning.Api.Requests.Exercise;

public class ServiceContainerRequest : ContainerRequest
{
    public List<string>? HealthcheckTest { get; init; }
}
