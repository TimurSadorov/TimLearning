using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Exercise;

public class ExerciseRequest
{
    [Required]
    public required Guid AppArchiveId { get; init; }

    [Required]
    public required MainAppContainerRequest AppContainer { get; init; }

    [Required]
    public required string RelativePathToDockerfile { get; init; }

    [Required]
    public required string[] RelativePathToInsertCode { get; init; }

    [Required]
    public required string InsertableCode { get; init; }

    [Required]
    public required List<ServiceContainerImageRequest> ServiceApps { get; init; }
}
