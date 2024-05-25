using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.CodeReview;

public record CodeReviewLessonResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    [property: Required] string Text
);
