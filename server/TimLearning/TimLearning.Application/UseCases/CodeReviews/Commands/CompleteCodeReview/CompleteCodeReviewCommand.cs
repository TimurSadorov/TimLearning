using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviews.Commands.CompleteCodeReview;

public record CompleteCodeReviewCommand(Guid CodeReviewId, bool IsSuccess, Guid CallingUserId)
    : IRequest,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.Mentor];
}
