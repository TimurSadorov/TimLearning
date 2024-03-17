using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class Module : IIdHolder<Guid>
{
    private Module() { }

    public Module(int order)
    {
        Order = order;
    }

    public Guid Id { get; init; }

    public required string Name { get; set; }

    public int? Order { get; private set; }

    public required Guid CourseId { get; init; }
    public Course Course { get; init; } = null!;

    public required bool IsDraft { get; set; }

    public bool IsDeleted { get; private set; }

    public void MarkDeleted()
    {
        IsDeleted = true;
        Order = null;
    }

    public void Restore(int order)
    {
        IsDeleted = false;
        Order = order;
    }

    public void SetOrder(int order)
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException("Cant set order, module is deleted.");
        }

        Order = order;
    }
}
