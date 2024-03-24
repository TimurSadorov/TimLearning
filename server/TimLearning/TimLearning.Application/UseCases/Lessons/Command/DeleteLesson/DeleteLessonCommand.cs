using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.Mediator.Pipelines.Transactional;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Lessons.Command.DeleteLesson;

public record DeleteLessonCommand(Guid LessonId, Guid CallingUserId)
    : IRequest,
        IAccessByRole,
        ITransactional
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
}
