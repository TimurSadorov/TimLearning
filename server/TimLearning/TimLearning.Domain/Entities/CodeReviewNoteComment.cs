using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class CodeReviewNoteComment : IIdHolder<Guid>
{
    public Guid Id { get; init; }

    public required Guid CodeReviewNoteId { get; init; }
    public CodeReviewNote CodeReviewNote { get; init; } = null!;

    public required Guid AuthorId { get; init; }
    public User Author { get; init; } = null!;

    public required string Text { get; set; }

    public bool Deleted { get; private set; }

    public required DateTimeOffset Added { get; init; }

    public void Delete()
    {
        Deleted = true;
    }
}
