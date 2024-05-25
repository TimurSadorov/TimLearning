using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.CodeReview;

public record UserSolutionResponse(
    [property: Required] string Code,
    [property: Required] DateTimeOffset Added
);
