using LinqSpecs.Core;

namespace TimLearning.Shared.LinqSpecs;

public static class SpecificationExtensions
{
    public static bool IsSatisfiedBy<T>(this Specification<T> spec, T entity)
    {
        return CompiledExpressionsCache<T, bool>.AsFunc(spec.ToExpression()).Invoke(entity);
    }
}
