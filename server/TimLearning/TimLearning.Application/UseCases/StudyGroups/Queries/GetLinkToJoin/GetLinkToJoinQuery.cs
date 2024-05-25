using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.StudyGroups.Queries.GetLinkToJoin;

public record GetLinkToJoinQuery(Guid StudyGroupId, Guid CallingUserId)
    : IRequest<string>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.Mentor];
}
