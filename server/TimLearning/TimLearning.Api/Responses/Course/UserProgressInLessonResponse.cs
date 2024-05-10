using System.ComponentModel.DataAnnotations;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Api.Responses.Course;

public record UserProgressInLessonResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    UserProgressType? UserProgress
);
