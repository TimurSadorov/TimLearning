namespace TimLearning.Api.ToDo.Requests.Docker;

public class ContainerSettingsRequest
{
    public string? Hostname { get; init; }

    public List<string>? HealthcheckTest { get; init; }

    public List<ContainerEnvRequest>? Envs { get; init; }
}
