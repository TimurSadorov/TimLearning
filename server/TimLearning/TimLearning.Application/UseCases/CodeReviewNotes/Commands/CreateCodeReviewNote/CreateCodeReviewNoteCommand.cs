using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.CodeReviewNotes.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Commands.CreateCodeReviewNote;

public record CreateCodeReviewNoteCommand(NewCodeReviewNote Dto, Guid CallingUserId)
    : IRequest<Guid>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.Mentor];
}
