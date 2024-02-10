using FluentValidation;
using TimLearning.Shared.Validation.FluentValidator.Extensions;

namespace TimLearning.Shared.Validation.FluentValidator.Validators;

public abstract class CombinedFluentAndSimpleValidator<T>
    : AbstractValidator<T>,
        ICombinedFluentAndSimpleValidator<T>
{
    public async Task ValidateAndThrowAsync(T entity, CancellationToken ct = default)
    {
        await ConfigureFluentRulesAsync(entity, ct);
        await this.ValidateAndThrowLocalizedExceptionAsync(entity, ct);

        await SimpleValidateAndThrowAsync(entity, ct);
    }

    protected abstract Task ConfigureFluentRulesAsync(T entity, CancellationToken ct);

    protected abstract Task SimpleValidateAndThrowAsync(T entity, CancellationToken ct);
}
