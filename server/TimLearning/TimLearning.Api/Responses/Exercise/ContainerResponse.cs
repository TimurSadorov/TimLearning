namespace TimLearning.Api.Responses.Exercise;

public class ContainerResponse
{
    public string? Hostname { get; init; }
    public List<ContainerEnvResponse>? Envs { get; init; }
}
