using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.CodeReviews.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviews.Queries.GetUserSolutionCodeReview;

public record GetUserSolutionCodeReviewQuery(Guid CodeReviewId, Guid CallingUserId)
    : IRequest<UserSolutionCodeReviewDto>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.Mentor];
}
