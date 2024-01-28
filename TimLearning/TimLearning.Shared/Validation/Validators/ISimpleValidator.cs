namespace TimLearning.Shared.Validation.Validators;

public interface ISimpleValidator<in TEntity>
{
    void ValidateAndThrow(TEntity entity);
}
