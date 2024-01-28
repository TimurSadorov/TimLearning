using System.Linq.Expressions;
using LinqSpecs.Core;
using TimLearning.Shared.BaseEntities;

namespace TimLearning.Shared.Specifications.Dynamic;

public class EntityByGuidIdSpecification<TEntity> : Specification<TEntity>
    where TEntity : IIdHolder<Guid>
{
    private readonly Guid _id;

    public EntityByGuidIdSpecification(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        return e => e.Id == _id;
    }
}
