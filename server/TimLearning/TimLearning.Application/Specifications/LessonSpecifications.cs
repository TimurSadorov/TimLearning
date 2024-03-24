using LinqSpecs.Core;
using TimLearning.Domain.Entities;

namespace TimLearning.Application.Specifications;

public static class LessonSpecifications
{
    public static Specification<Lesson> BeforeLesson(Guid moduleId, Guid? id) =>
        new AdHocSpecification<Lesson>(
            l => l.ModuleId == moduleId && l.NextLessonId == id && l.IsDeleted == false
        );
}
