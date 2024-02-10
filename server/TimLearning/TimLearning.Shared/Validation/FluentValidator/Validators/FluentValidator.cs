using FluentValidation;
using FluentValidation.Results;

namespace TimLearning.Shared.Validation.FluentValidator.Validators;

public abstract class FluentValidator<T> : AbstractValidator<T>
{
    public override async Task<ValidationResult> ValidateAsync(
        ValidationContext<T> context,
        CancellationToken cancellation = default
    )
    {
        
        var entityToValidate = context.InstanceToValidate;
        await ConfigureRulesAsync(entityToValidate, cancellation);

        return await base.ValidateAsync(context, cancellation);
    }

    protected abstract Task ConfigureRulesAsync(T entity, CancellationToken ct = default);
}
