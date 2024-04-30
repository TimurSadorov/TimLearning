using System.ComponentModel.DataAnnotations;

namespace TimLearning.DockerManager.ApiClient.V1.Response;

public record V1AppTestResponse(
    [property: Required] V1AppTestingStatus Status,
    string? ErrorMessage
);
