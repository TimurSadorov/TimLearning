using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class Course : IIdHolder<Guid>
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public required string Description { get; set; }
    public required bool IsDraft  { get; set; }
    public bool IsDeleted  { get; set; }
}