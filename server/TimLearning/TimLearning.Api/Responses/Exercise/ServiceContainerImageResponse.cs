using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Exercise;

public class ServiceContainerImageResponse
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Tag { get; init; }

    [Required]
    public required ServiceContainerResponse Container { get; init; }
}
