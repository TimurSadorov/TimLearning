using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Course;

public record UserProgressInModuleResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    [property: Required] int CompletionPercentage,
    [property: Required] List<UserProgressInLessonResponse> Lessons
);
