using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Domain.Entities;

public class UserRole
{
    public required UserRoleType Type { get; init; }

    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
}
