using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Modules.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Modules.Commands.UpdateModule;

public record UpdateModuleCommand(UpdatedModuleDto Dto, Guid CallingUserId)
    : IRequest,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
}
