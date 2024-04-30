using System.ComponentModel.DataAnnotations;

namespace TimLearning.DockerManager.ApiClient.V1.Request;

public class V1ServiceContainerImageRequest
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Tag { get; init; }

    [Required]
    public required V1ServiceContainerRequest Container { get; init; }
}
