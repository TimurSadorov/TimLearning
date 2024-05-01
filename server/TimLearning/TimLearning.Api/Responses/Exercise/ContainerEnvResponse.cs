using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Exercise;

public class ContainerEnvResponse
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Value { get; init; }
}
