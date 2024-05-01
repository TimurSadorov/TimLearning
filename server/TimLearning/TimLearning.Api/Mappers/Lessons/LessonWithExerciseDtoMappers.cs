using TimLearning.Api.Mappers.Exercise;
using TimLearning.Api.Responses.Exercise;
using TimLearning.Api.Responses.Lesson;
using TimLearning.Application.UseCases.Lessons.Dto;

namespace TimLearning.Api.Mappers.Lessons;

public static class LessonWithExerciseDtoMappers
{
    public static LessonWithExerciseResponse ToResponse(this LessonWithExerciseDto dto)
    {
        var exercise = dto.Exercise;
        return new LessonWithExerciseResponse(
            dto.Name,
            dto.Text,
            exercise is null
                ? null
                : new ExerciseResponse
                {
                    AppArchiveId = exercise.AppArchiveId,
                    InsertableCode = exercise.InsertableCode,
                    RelativePathToDockerfile = exercise.RelativePathToDockerfile,
                    RelativePathToInsertCode = exercise.RelativePathToInsertCode,
                    AppContainer = new MainAppContainerResponse
                    {
                        Hostname = exercise.AppContainerData.Hostname,
                        Envs = exercise.AppContainerData.Envs?.Select(e => e.ToResponse()).ToList()
                    },
                    ServiceApps = exercise.ServiceApps
                        .Select(
                            a =>
                                new ServiceContainerImageResponse
                                {
                                    Name = a.Name,
                                    Tag = a.Tag,
                                    Container = new ServiceContainerResponse
                                    {
                                        Hostname = a.ContainerData.Hostname,
                                        Envs = a.ContainerData.Envs
                                            ?.Select(e => e.ToResponse())
                                            .ToList(),
                                        HealthcheckTest = a.ContainerData.HealthcheckTest
                                    }
                                }
                        )
                        .ToList()
                }
        );
    }
}
