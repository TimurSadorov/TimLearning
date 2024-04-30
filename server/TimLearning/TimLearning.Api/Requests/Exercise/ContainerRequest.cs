namespace TimLearning.Api.Requests.Exercise;

public class ContainerRequest
{
    public string? Hostname { get; init; }
    public List<ContainerEnvRequest>? Envs { get; init; }
}
