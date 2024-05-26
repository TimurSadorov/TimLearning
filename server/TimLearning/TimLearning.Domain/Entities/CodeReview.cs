using TimLearning.Domain.Entities.Enums;
using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class CodeReview : IIdHolder<Guid>
{
    private CodeReview() { }

    public CodeReview(Guid userSolutionId)
    {
        UserSolutionId = userSolutionId;
    }

    public Guid Id { get; init; }

    public required Guid GroupStudentId { get; init; }
    public GroupStudent GroupStudent { get; init; } = null!;

    public Guid UserSolutionId { get; private set; }
    public UserSolution UserSolution { get; private set; } = null!;

    public CodeReviewStatus Status { get; private set; } = CodeReviewStatus.Pending;

    public DateTimeOffset? Completed { get; private set; }

    public List<CodeReviewNote> Notes { get; init; } = null!;

    public void SetUserSolutionId(Guid id)
    {
        if (Status is not CodeReviewStatus.Pending)
        {
            throw new InvalidOperationException(
                $"Cant set user solution. Review is not in {CodeReviewStatus.Pending.ToString()} status."
            );
        }

        UserSolutionId = id;
    }

    public void Start()
    {
        if (Status is not CodeReviewStatus.Pending)
        {
            throw new InvalidOperationException(
                $"Cant start review. Review is not in {CodeReviewStatus.Pending.ToString()} status."
            );
        }

        Status = CodeReviewStatus.Started;
    }

    public void Complete(DateTimeOffset now)
    {
        if (Status is not CodeReviewStatus.Started)
        {
            throw new InvalidOperationException(
                $"Cant complete review. Review is not in {CodeReviewStatus.Started.ToString()} status."
            );
        }

        Status = CodeReviewStatus.Completed;
        Completed = now;
    }

    public void Reject(DateTimeOffset now)
    {
        if (Status is not CodeReviewStatus.Started)
        {
            throw new InvalidOperationException(
                $"Cant reject review. Review is not in {CodeReviewStatus.Started.ToString()} status."
            );
        }

        Status = CodeReviewStatus.Rejected;
        Completed = now;
    }
}
