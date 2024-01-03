using FluentValidation;
using FluentValidation.Results;
using TimLearning.Shared.Validation.Exceptions;

namespace TimLearning.Shared.Validation.FluentValidator.Extensions;

public static class ValidatorExtensions
{
    public static void ValidateAndThrowWithLocalizedErrors<T>(
        this IValidator<T> validator,
        T entityToValidate
    )
    {
        var validationResult = validator.Validate(entityToValidate);
        ThrowLocalizedValidationExceptionIfNotValid(validationResult);
    }

    public static async Task ValidateAndThrowWithLocalizedErrorsAsync<T>(
        this IValidator<T> validator,
        T entityToValidate
    )
    {
        var validationResult = await validator.ValidateAsync(entityToValidate);
        ThrowLocalizedValidationExceptionIfNotValid(validationResult);
    }

    private static void ThrowLocalizedValidationExceptionIfNotValid(
        ValidationResult validationResult
    )
    {
        if (validationResult.IsValid is false)
        {
            throw new LocalizedValidationException(
                validationResult
                    .Errors.ToLookup(e => e.PropertyName, e => e.ErrorMessage)
                    .Select(
                        propErrors =>
                            new PropertyValidationResult(propErrors.Key, propErrors.ToList())
                    )
                    .ToList()
            );
        }
    }
}
