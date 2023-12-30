using System.Text.Json.Serialization;

namespace TimLearning.Shared.AspNet.Filters.Validation;

public record ErrorDetail(
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("errors")] Dictionary<string, string[]> Errors
);
