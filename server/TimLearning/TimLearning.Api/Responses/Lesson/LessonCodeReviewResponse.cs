using System.ComponentModel.DataAnnotations;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Api.Responses.Lesson;

public record LessonCodeReviewResponse(
    [property: Required] Guid Id,
    [property: Required] CodeReviewStatus Status
);
