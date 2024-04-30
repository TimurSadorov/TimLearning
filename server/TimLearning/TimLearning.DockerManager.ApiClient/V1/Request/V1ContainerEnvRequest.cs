using System.ComponentModel.DataAnnotations;

namespace TimLearning.DockerManager.ApiClient.V1.Request;

public class V1ContainerEnvRequest
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Value { get; init; }
}
