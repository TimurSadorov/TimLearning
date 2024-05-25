using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class StudyGroup : IIdHolder<Guid>
{
    public Guid Id { get; init; }

    public required Guid CourseId { get; init; }
    public Course Course { get; init; } = null!;

    public required Guid MentorId { get; init; }
    public User Mentor { get; init; } = null!;

    public required string Name { get; set; }

    public required bool IsActive { get; set; }

    public required DateTimeOffset Added { get; init; }
}
