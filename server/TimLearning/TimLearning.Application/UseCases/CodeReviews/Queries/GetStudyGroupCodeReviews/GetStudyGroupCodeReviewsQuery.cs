using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.CodeReviews.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviews.Queries.GetStudyGroupCodeReviews;

public record GetStudyGroupCodeReviewsQuery(StudyGroupCodeReviewsFiltersDto Dto, Guid CallingUserId)
    : IRequest<List<StudyGroupCodeReviewDto>>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.Mentor];
}
