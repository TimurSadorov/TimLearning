using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class Module : IIdHolder<Guid>
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required Guid CourseId { get; init; }
    public Course Course { get; init; } = null!;

    public Guid? NextModuleId { get; set; }
    public Module? NextModule { get; set; }
}
