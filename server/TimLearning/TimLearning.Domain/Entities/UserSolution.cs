using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class UserSolution : IIdHolder<Guid>
{
    public Guid Id { get; init; }

    public required Guid UserId { get; init; }
    public User User { get; init; } = null!;

    public required Guid ExerciseId { get; init; }
    public Exercise Exercise { get; init; } = null!;

    public required string Code { get; init; }

    public required DateTimeOffset Added { get; init; }

    public CodeReview? CodeReview { get; init; }
}
