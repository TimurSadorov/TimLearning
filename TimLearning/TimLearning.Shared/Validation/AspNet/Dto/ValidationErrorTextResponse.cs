using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TimLearning.Shared.Validation.Exceptions.Localized.Errors;

namespace TimLearning.Shared.Validation.AspNet.Dto;

public record ValidationErrorTextResponse(
    [property: JsonPropertyName("message"), Required] string Message
) : ValidationErrorResponse
{
    public ValidationErrorTextResponse(SimpleTextError textError)
        : this(textError.Message) { }
};
