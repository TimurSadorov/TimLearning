using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Lesson;

public record UserLessonResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    [property: Required] string Text,
    UserExerciseResponse? Exercise
);
