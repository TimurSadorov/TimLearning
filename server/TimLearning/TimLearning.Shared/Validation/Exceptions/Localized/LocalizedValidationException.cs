using System.Diagnostics.CodeAnalysis;
using TimLearning.Shared.Validation.Exceptions.Localized.Errors;

namespace TimLearning.Shared.Validation.Exceptions.Localized;

public class LocalizedValidationException(LocalizedError error) : Exception
{
    public LocalizedError Error { get; } = error;

    [DoesNotReturn]
    public static void ThrowNotFoundTextError()
    {
        ThrowSimpleTextError("Сущность не найдена.");
    }

    [DoesNotReturn]
    public static void ThrowSimpleTextError(string message)
    {
        throw new LocalizedValidationException(new SimpleTextError(message));
    }

    [DoesNotReturn]
    public static void ThrowModelError(List<PropertyError> propertiesErrors)
    {
        throw new LocalizedValidationException(new ModelError(propertiesErrors));
    }
}
