using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Domain.Entities;

public class UserRole
{
    public required UserRoleType Type { get; set; }

    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
}
