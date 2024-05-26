using TimLearning.Domain.Entities.Data;
using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class CodeReviewNote : IIdHolder<Guid>
{
    public Guid Id { get; init; }

    public required Guid CodeReviewId { get; init; }
    public CodeReview CodeReview { get; init; } = null!;

    public required CodeReviewNotePositionData StartPosition { get; init; }

    public required CodeReviewNotePositionData EndPosition { get; init; }

    public bool Deleted { get; private set; }

    public required DateTimeOffset Added { get; init; }

    public List<CodeReviewNoteComment> Comments { get; init; } = null!;

    public void Delete()
    {
        Deleted = true;
    }
}
