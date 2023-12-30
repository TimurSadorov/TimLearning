using FluentValidation;
using FluentValidation.Results;

namespace TimLearning.Shared.FluentValidator.Extensions;

public static class ValidatorExtensions
{
    public static void ValidateAndThrowWithMessage<T>(
        this IValidator<T> validator,
        T entityToValidate,
        string messageException = "Model validation error."
    )
    {
        var validationResult = validator.Validate(entityToValidate);
        ThrowValidationExceptionIfNotValid(validationResult, messageException);
    }

    public static async Task ValidateAndThrowWithMessageAsync<T>(
        this IValidator<T> validator,
        T entityToValidate,
        string messageException = "Model validation error."
    )
    {
        var validationResult = await validator.ValidateAsync(entityToValidate);
        ThrowValidationExceptionIfNotValid(validationResult, messageException);
    }

    private static void ThrowValidationExceptionIfNotValid(
        ValidationResult validationResult,
        string messageException
    )
    {
        if (!validationResult.IsValid)
        {
            throw new ValidationException(messageException, validationResult.Errors);
        }
    }
}
