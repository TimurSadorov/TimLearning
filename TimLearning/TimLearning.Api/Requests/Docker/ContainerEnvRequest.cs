using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Docker;

public record ContainerEnvRequest
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Value { get; init; }
};
