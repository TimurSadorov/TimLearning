using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.Mediator.Pipelines.Transactional;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Lessons.Command.MoveLesson;

public record MoveLessonCommand(LessonMovementDto Dto, Guid CallingUserId)
    : IRequest,
        IAccessByRole,
        ITransactional
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
}
