using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.CodeReviewNoteComments.Dto;
using TimLearning.Domain.Access;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.CreateCodeReviewNoteComment;

public record CreateCodeReviewNoteCommentCommand(
    NewCodeReviewNoteCommentDto Dto,
    Guid CallingUserId
) : IRequest, IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles => AccessGroup.Everyone;
}
