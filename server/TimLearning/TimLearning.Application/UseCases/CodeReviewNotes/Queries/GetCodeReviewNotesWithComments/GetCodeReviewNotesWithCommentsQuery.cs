using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.CodeReviewNotes.Dto;
using TimLearning.Domain.Access;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Queries.GetCodeReviewNotesWithComments;

public record GetCodeReviewNotesWithCommentsQuery(Guid CodeReviewId, Guid CallingUserId)
    : IRequest<List<CodeReviewNoteWithCommentDto>>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles => AccessGroup.Everyone;
}
