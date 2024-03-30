using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Modules.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Modules.Queries.GetModuleAllData;

public record GetModuleAllDataQuery(Guid ModuleId, Guid CallingUserId)
    : IRequest<ModuleAllDataDto>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
}
