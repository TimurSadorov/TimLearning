namespace TimLearning.Shared.Validation.Validators;

public interface IAsyncSimpleValidator<in TEntity>
{
    Task ValidateAndThrowAsync(TEntity entity);
}
