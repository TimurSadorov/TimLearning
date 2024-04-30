using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Exercise;

public class ContainerEnvRequest
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Value { get; init; }
}
