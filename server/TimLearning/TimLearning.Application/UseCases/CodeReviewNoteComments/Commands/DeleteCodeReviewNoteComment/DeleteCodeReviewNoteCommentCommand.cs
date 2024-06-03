using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.Mediator.Pipelines.Transactional;
using TimLearning.Domain.Access;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.DeleteCodeReviewNoteComment;

public record DeleteCodeReviewNoteCommentCommand(Guid Id, Guid CallingUserId)
    : IRequest,
        IAccessByRole,
        ITransactional
{
    public static IEnumerable<UserRoleType> ForRoles => AccessGroup.Everyone;
}
