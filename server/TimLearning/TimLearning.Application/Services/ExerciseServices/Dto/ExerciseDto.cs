using TimLearning.Domain.Entities.Data;

namespace TimLearning.Application.Services.ExerciseServices.Dto;

public record ExerciseDto(
    Guid AppArchiveId,
    MainAppContainerData AppContainerData,
    string RelativePathToDockerfile,
    string[] RelativePathToInsertCode,
    string InsertableCode,
    List<ServiceContainerImageData> ServiceApps
);
