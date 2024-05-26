using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviews.Commands.StartCodeReview;

public record StartCodeReviewCommand(Guid CodeReviewId, Guid CallingUserId)
    : IRequest,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.Mentor];
}
