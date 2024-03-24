using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Lessons.Command.RestoreLesson;

public record RestoreLessonCommand(Guid LessonId, Guid CallingUserId) : IRequest, IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
}