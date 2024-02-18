using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TimLearning.Shared.Validation.Exceptions.Localized.Errors;

namespace TimLearning.Shared.AspNet.Validations.Dto;

public record ModelValidationErrorResponse(
    [property: JsonPropertyName("propertiesErrors"), Required]
        Dictionary<string, List<string>> PropertiesErrors
) : ValidationErrorResponse
{
    public ModelValidationErrorResponse(ModelError modelError)
        : this(modelError.PropertiesErrors.ToDictionary(p => p.Name, p => p.Errors)) { }
};
