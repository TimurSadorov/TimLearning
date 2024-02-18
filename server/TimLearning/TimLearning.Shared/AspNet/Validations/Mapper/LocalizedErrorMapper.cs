using TimLearning.Shared.AspNet.Validations.Dto;
using TimLearning.Shared.Validation.Exceptions.Localized.Errors;

namespace TimLearning.Shared.AspNet.Validations.Mapper;

public static class LocalizedErrorMapper
{
    public static ValidationErrorResponse ToValidationErrorResponse(
        this LocalizedError localizedError
    )
    {
        return localizedError switch
        {
            ModelError modelError => new ModelValidationErrorResponse(modelError),
            SimpleTextError simpleError => new ValidationErrorTextResponse(simpleError),
            _
                => throw new ArgumentException(
                    $"Unknown validation error type:{localizedError.GetType().FullName}"
                )
        };
    }
}
