using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.StudyGroups.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.StudyGroups.Queries.FindStudyGroups;

public record FindStudyGroupsQuery(StudyGroupsFindDto Dto, Guid CallingUserId)
    : IRequest<List<StudyGroupDto>>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.Mentor];
}
