using System.Text.Json.Serialization;

namespace TimLearning.Domain.Entities.Data;

public record ServiceContainerImageData
{
    [JsonRequired]
    public required string Name { get; init; }

    [JsonRequired]
    public required string Tag { get; init; }

    [JsonRequired]
    public required ServiceContainerData ContainerData { get; init; }
}
