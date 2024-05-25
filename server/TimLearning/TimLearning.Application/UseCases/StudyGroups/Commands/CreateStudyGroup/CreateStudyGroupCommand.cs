using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.StudyGroups.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.StudyGroups.Commands.CreateStudyGroup;

public record CreateStudyGroupCommand(NewStudyGroupDto Dto, Guid CallingUserId)
    : IRequest<Guid>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.Mentor];
}
