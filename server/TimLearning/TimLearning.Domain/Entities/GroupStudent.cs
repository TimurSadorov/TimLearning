using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class GroupStudent : IIdHolder<Guid>
{
    public Guid Id { get; init; }

    public required Guid StudyGroupId { get; init; }
    public StudyGroup StudyGroup { get; init; } = null!;

    public required Guid UserId { get; init; }
    public User User { get; init; } = null!;

    public required DateTimeOffset Added { get; init; }
}
