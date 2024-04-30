namespace TimLearning.Domain.Entities.Data;

public record ServiceContainerData : ContainerData
{
    public List<string>? HealthcheckTest { get; init; }
}
