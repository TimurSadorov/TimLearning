namespace TimLearning.Shared.App.Exceptions;

public record AppValidationException<TError>(
    TError Error,
    string Message,
    List<PropertyValidationResult> PropertiesErrors
)
    where TError : Enum;
