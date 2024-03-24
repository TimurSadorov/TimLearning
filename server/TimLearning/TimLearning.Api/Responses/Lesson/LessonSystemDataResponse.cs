using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Lesson;

public record LessonSystemDataResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    [property: Required] bool IsDraft
);
