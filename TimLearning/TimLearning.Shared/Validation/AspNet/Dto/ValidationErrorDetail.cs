using System.Text.Json.Serialization;

namespace TimLearning.Shared.Validation.AspNet.Dto;

public record ValidationErrorDetail(
    [property: JsonPropertyName("propertiesErrors")]
        Dictionary<string, List<string>> PropertiesErrors
);
