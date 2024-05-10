using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Domain.Entities;

public class UserProgress
{
    public required Guid UserId { get; init; }
    public User User { get; init; } = null!;

    public required Guid LessonId { get; init; }
    public Lesson Lesson { get; init; } = null!;

    public UserProgressType Type { get; private set; } = UserProgressType.Viewed;

    public void Complete()
    {
        Type = UserProgressType.Completed;
    }
}
