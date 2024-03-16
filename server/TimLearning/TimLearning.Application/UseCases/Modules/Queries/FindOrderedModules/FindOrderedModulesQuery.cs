using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Modules.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Modules.Queries.FindOrderedModules;

public record FindOrderedModulesQuery(ModulesFindDto Dto, Guid CallingUserId)
    : IRequest<List<OrderedModuleDto>>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles => [UserRoleType.ContentCreator];
}
