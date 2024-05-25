using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.StudyGroups.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.StudyGroups.Commands.UpdateStudyGroup;

public record UpdateStudyGroupCommand(UpdatableStudyGroupDto Dto, Guid CallingUserId)
    : IRequest,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.Mentor];
}
