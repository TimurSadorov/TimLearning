using TimLearning.Api.Mappers.Exercise;
using TimLearning.Api.Requests.Lesson;
using TimLearning.Application.Services.ExerciseServices.Dto;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Entities.Data;

namespace TimLearning.Api.Mappers.Lessons;

public static class UpdateLessonRequestMappers
{
    public static UpdatedLessonDto ToDto(this UpdateLessonRequest request, Guid lessonId)
    {
        var exercise = request.Exercise?.Value;

        return new UpdatedLessonDto(
            lessonId,
            request.Name,
            request.Text,
            request.IsDraft,
            request.Exercise is null
                ? null
                : new LessonExerciseDto(
                    exercise is null
                        ? null
                        : new ExerciseDto(
                            exercise.AppArchiveId,
                            new MainAppContainerData
                            {
                                Hostname = exercise.AppContainer.Hostname,
                                Envs = exercise.AppContainer.Envs?.Select(e => e.ToData()).ToList()
                            },
                            exercise.RelativePathToDockerfile,
                            exercise.RelativePathToInsertCode,
                            exercise.InsertableCode,
                            exercise.ServiceApps
                                .Select(
                                    i =>
                                        new ServiceContainerImageData
                                        {
                                            Name = i.Name,
                                            Tag = i.Tag,
                                            ContainerData = new ServiceContainerData
                                            {
                                                Hostname = i.Container.Hostname,
                                                HealthcheckTest =
                                                    i.Container.HealthcheckTest?.ToList(),
                                                Envs = i.Container.Envs
                                                    ?.Select(e => e.ToData())
                                                    .ToList()
                                            }
                                        }
                                )
                                .ToList()
                        )
                )
        );
    }
}
