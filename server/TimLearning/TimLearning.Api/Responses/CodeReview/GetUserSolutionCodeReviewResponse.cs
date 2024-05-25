using System.ComponentModel.DataAnnotations;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Api.Responses.CodeReview;

public record GetUserSolutionCodeReviewResponse(
    [property: Required] CodeReviewStatus Status,
    [property: Required] string UserEmail,
    [property: Required] CodeReviewLessonResponse Lesson,
    [property: Required] UserSolutionResponse Solution,
    [property: Required] string StandardCode
);
