using System.Text.Json.Serialization;

namespace TimLearning.Domain.Entities.Data;

public record ContainerEnvData
{
    [JsonRequired]
    public required string Name { get; init; }

    [JsonRequired]
    public required string Value { get; init; }
}
