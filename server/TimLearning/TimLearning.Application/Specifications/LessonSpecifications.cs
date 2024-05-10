using LinqSpecs.Core;
using TimLearning.Domain.Entities;

namespace TimLearning.Application.Specifications;

public static class LessonSpecifications
{
    public static Specification<Lesson> BeforeLesson(Guid moduleId, Guid? id) =>
        new AdHocSpecification<Lesson>(
            l => l.ModuleId == moduleId && l.NextLessonId == id && l.IsDeleted == false
        );

    public static Specification<Lesson> UserAvailable { get; } =
        new AdHocSpecification<Lesson>(l => l.IsDraft == false && l.IsDeleted == false);

    public static Specification<Lesson> HasOrder { get; } =
        new AdHocSpecification<Lesson>(l => l.IsDeleted == false);

    public static Specification<Lesson> IsPractical { get; } =
        new AdHocSpecification<Lesson>(l => l.ExerciseId != null);
}
