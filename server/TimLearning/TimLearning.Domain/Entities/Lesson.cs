using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class Lesson : IIdHolder<Guid>
{
    private Lesson() { }

    public Lesson(Lesson? previousLesson)
    {
        previousLesson?.EnsureChangePositionLesson();
        PreviousLesson = previousLesson;
    }

    public Guid Id { get; init; }
    public required string Name { get; set; }
    public required string Text { get; set; }

    public Guid? NextLessonId { get; private set; }
    public Lesson? NextLesson { get; private set; }
    public Lesson? PreviousLesson { get; private set; }

    public required Guid ModuleId { get; init; }
    public Module Module { get; init; } = null!;

    public required bool IsDraft { get; set; }
    public bool IsDeleted { get; private set; }

    public void Delete()
    {
        IsDeleted = true;
        NextLessonId = null;
    }

    public void Restore(Lesson? previousLesson)
    {
        previousLesson?.EnsureChangePositionLesson();
        if (IsDeleted is false)
        {
            throw new InvalidOperationException("Lesson was not deleted.");
        }

        IsDeleted = false;
        PreviousLesson = previousLesson;
        IsDraft = true;
    }

    public void ClearNextValue()
    {
        EnsureChangePositionLesson();
        NextLessonId = null;
    }

    public void SetNextLesson(Guid? value)
    {
        EnsureChangePositionLesson();
        NextLessonId = value;
    }

    public void SetNextLesson(Lesson? value)
    {
        EnsureChangePositionLesson();
        value?.EnsureChangePositionLesson();
        NextLesson = value;
    }

    public void SetPreviousLesson(Lesson? value)
    {
        EnsureChangePositionLesson();
        value?.EnsureChangePositionLesson();
        PreviousLesson = value;
    }

    private void EnsureChangePositionLesson()
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException("Cant set position, lesson is deleted.");
        }
    }
}
