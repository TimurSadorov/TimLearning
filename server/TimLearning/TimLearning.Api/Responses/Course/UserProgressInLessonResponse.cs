using System.ComponentModel.DataAnnotations;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Api.Responses.Course;

public record UserProgressInLessonResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    [property: Required] bool IsPractical,
    UserProgressType? UserProgress
);
