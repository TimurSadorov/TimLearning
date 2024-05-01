using TimLearning.Application.Services.ExerciseServices.Dto;
using TimLearning.Domain.Entities;

namespace TimLearning.Application.Services.ExerciseServices.Mappers;

public static class ExerciseMappers
{
    public static ExerciseDto ToDto(this Exercise exercise)
    {
        return new ExerciseDto(
            exercise.AppArchiveId,
            exercise.AppContainerData,
            exercise.RelativePathToDockerfile,
            exercise.RelativePathToInsertCode,
            exercise.StandardCode,
            exercise.ServiceApps
        );
    }
}
