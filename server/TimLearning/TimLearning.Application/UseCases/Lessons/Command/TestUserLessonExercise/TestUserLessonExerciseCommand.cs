using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Access;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Lessons.Command.TestUserLessonExercise;

public record TestUserLessonExerciseCommand(Guid LessonId, string Code, Guid CallingUserId)
    : IRequest<UserExerciseTestingResultDto>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles => AccessGroup.Everyone;
}
