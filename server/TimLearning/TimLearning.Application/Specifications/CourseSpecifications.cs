using LinqSpecs.Core;
using TimLearning.Domain.Entities;

namespace TimLearning.Application.Specifications;

public static class CourseSpecifications
{
    public static Specification<Course> UserAvailable { get; } =
        new AdHocSpecification<Course>(
            course => course.IsDraft == false && course.IsDeleted == false
        );
}
