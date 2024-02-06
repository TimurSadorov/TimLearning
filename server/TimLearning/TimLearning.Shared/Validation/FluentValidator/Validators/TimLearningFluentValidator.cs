using FluentValidation;
using FluentValidation.Results;

namespace TimLearning.Shared.Validation.FluentValidator.Validators;

public abstract class TimLearningFluentValidator<T> : AbstractValidator<T>
{
    public override async Task<ValidationResult> ValidateAsync(
        ValidationContext<T> context,
        CancellationToken cancellation = default
    )
    {
        var entityToValidate = context.InstanceToValidate;
        await PrepareDataAsync(entityToValidate);

        return await base.ValidateAsync(context, cancellation);
    }

    protected virtual Task PrepareDataAsync(T entity)
    {
        return Task.CompletedTask;
    }

    public override ValidationResult Validate(ValidationContext<T> context)
    {
        var entityToValidate = context.InstanceToValidate;
        PrepareData(entityToValidate);

        return base.Validate(context);
    }

    protected virtual void PrepareData(T entity) { }
}
