using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Access;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Lessons.Queries.GetUserLessonExerciseAppFile;

public record GetUserLessonExerciseAppFileQuery(Guid LessonId, Guid CallingUserId)
    : IRequest<ExerciseAppFileDto>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles => AccessGroup.Everyone;
}
