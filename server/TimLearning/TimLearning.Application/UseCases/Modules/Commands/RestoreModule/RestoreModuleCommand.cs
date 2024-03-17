using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Modules.Commands.RestoreModule;

public record RestoreModuleCommand(Guid ModuleId, Guid CallingUserId) : IRequest, IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
}