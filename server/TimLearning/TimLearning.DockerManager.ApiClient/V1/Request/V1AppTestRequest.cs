using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TimLearning.DockerManager.ApiClient.V1.Request;

public class V1AppTestRequest
{
    [Required]
    public required IFormFile App { get; init; }

    [Required]
    public required V1MainAppContainerRequest AppContainer { get; init; }

    [Required]
    public required string RelativePathToDockerfile { get; init; }

    [Required]
    public required IReadOnlyCollection<V1ServiceContainerImageRequest> ServiceApps { get; init; }
}
