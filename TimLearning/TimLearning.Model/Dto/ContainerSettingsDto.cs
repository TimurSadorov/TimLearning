namespace TimLearning.Model.Dto;

public class ContainerSettingsDto
{
    public string? Hostname { get; init; }

    public List<string>? HealthcheckTest { get; init; }

    public List<ContainerEnvDto>? Envs { get; init; }
}
