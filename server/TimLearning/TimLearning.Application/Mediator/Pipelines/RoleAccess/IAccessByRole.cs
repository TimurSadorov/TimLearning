using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.Mediator.Pipelines.RoleAccess;

public interface IAccessByRole
{
    static abstract IEnumerable<UserRoleType> ForRoles { get; }

    public Guid CallingUserId { get; }
}
