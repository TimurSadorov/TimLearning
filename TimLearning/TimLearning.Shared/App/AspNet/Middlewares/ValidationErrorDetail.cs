using System.Text.Json.Serialization;

namespace TimLearning.Shared.App.AspNet.Middlewares;

public record ValidationErrorDetail<TError>(
    [property: JsonPropertyName("error")] TError Error,
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("propertiesErrors")] Dictionary<string, string[]> PropertiesErrors
)
    where TError : Enum;
