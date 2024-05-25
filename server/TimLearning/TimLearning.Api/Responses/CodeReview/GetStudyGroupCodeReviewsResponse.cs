using System.ComponentModel.DataAnnotations;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Api.Responses.CodeReview;

public record GetStudyGroupCodeReviewsResponse(
    [property: Required] Guid Id,
    [property: Required] CodeReviewStatus Status,
    DateTimeOffset? Completed,
    [property: Required] string UserEmail,
    [property: Required] string ModuleName,
    [property: Required] string LessonName
);
