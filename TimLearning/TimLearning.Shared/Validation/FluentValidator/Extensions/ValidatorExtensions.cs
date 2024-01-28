using FluentValidation;
using FluentValidation.Results;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Exceptions.Localized.Errors;

namespace TimLearning.Shared.Validation.FluentValidator.Extensions;

public static class ValidatorExtensions
{
    public static void ValidateAndThrowLocalizedException<T>(
        this IValidator<T> validator,
        T entityToValidate
    )
    {
        var validationResult = validator.Validate(entityToValidate);
        ThrowLocalizedValidationExceptionIfNotValid(validationResult);
    }

    public static async Task ValidateAndThrowLocalizedExceptionAsync<T>(
        this IValidator<T> validator,
        T entityToValidate,
        CancellationToken ct = default
    )
    {
        var validationResult = await validator.ValidateAsync(entityToValidate, ct);
        ThrowLocalizedValidationExceptionIfNotValid(validationResult);
    }

    private static void ThrowLocalizedValidationExceptionIfNotValid(
        ValidationResult validationResult
    )
    {
        if (validationResult.IsValid is false)
        {
            LocalizedValidationException.ThrowWithModelError(
                validationResult.Errors
                    .ToLookup(e => e.PropertyName, e => e.ErrorMessage)
                    .Select(
                        propErrors =>
                            new PropertyError(propErrors.Key, propErrors.ToList())
                    )
                    .ToList()
            );
        }
    }
}
