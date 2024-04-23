using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class StoredFile : IIdHolder<Guid>
{
    public Guid Id { get; init; }

    public required DateTimeOffset Added { get; init; }

    public required Guid AddedById { get; init; }
    public User AddedBy { get; init; } = null!;
}
