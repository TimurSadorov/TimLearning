using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class Module : IIdHolder<Guid>
{
    public Guid Id { get; init; }

    public required string Name { get; set; }

    public required int? Order { get; set; }

    public required Guid CourseId { get; init; }
    public Course Course { get; init; } = null!;

    public required bool IsDraft { get; set; }

    public required bool IsDeleted { get; set; }
}
