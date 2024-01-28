namespace TimLearning.Shared.Validation.Exceptions.Localized.Errors;

public class ModelError(List<PropertyError> propertiesErrors)
    : LocalizedError
{
    public IEnumerable<PropertyError> PropertiesErrors { get; } = propertiesErrors;
}
