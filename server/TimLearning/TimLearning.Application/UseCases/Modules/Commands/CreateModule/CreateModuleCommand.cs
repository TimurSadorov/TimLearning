using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Modules.Commands.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Modules.Commands.CreateModule;

public record CreateModuleCommand(NewModuleDto Dto, Guid CallingUserId) : IRequest, IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles => [UserRoleType.ContentCreator];
}
