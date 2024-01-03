namespace TimLearning.Shared.Validation.Exceptions;

public class LocalizedValidationException(List<PropertyValidationResult> propertiesErrors) : Exception
{
    public IEnumerable<PropertyValidationResult> PropertiesErrors { get; } = propertiesErrors;
}
