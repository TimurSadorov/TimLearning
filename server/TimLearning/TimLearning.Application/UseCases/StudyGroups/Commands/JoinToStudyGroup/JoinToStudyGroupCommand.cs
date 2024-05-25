using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.StudyGroups.Dto;
using TimLearning.Domain.Access;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.StudyGroups.Commands.JoinToStudyGroup;

public record JoinToStudyGroupCommand(JoiningDataDto Dto, Guid CallingUserId)
    : IRequest,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles => AccessGroup.Everyone;
}
