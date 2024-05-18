using LinqSpecs.Core;
using TimLearning.Domain.Entities;

namespace TimLearning.Application.Specifications;

public static class ModuleSpecifications
{
    public static Specification<Module> UserAvailable { get; } =
        new AdHocSpecification<Module>(module =>
            module.IsDraft == false && module.IsDeleted == false
        );
}
