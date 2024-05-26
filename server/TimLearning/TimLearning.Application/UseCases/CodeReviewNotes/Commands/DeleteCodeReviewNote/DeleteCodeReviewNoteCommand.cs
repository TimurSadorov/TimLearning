using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Commands.DeleteCodeReviewNote;

public record DeleteCodeReviewNoteCommand(Guid Id, Guid CallingUserId) : IRequest, IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.Mentor];
}
