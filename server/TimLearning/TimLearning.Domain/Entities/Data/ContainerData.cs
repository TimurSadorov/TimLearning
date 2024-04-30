namespace TimLearning.Domain.Entities.Data;

public record ContainerData
{
    public string? Hostname { get; init; }
    public List<ContainerEnvData>? Envs { get; init; }
}
