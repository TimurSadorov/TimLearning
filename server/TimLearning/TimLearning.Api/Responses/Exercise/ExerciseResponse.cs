using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Exercise;

public class ExerciseResponse
{
    [Required]
    public required Guid AppArchiveId { get; init; }

    [Required]
    public required MainAppContainerResponse AppContainer { get; init; }

    [Required]
    public required string RelativePathToDockerfile { get; init; }

    [Required]
    public required string[] RelativePathToInsertCode { get; init; }

    [Required]
    public required string InsertableCode { get; init; }

    [Required]
    public required List<ServiceContainerImageResponse> ServiceApps { get; init; }
}
