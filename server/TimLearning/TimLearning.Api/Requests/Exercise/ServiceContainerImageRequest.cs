using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Exercise;

public class ServiceContainerImageRequest
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Tag { get; init; }

    [Required]
    public required ServiceContainerRequest Container { get; init; }
}
