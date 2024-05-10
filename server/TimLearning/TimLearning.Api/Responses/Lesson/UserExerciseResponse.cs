using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Lesson;

public record UserExerciseResponse(
    [property: Required] Guid AppArchiveId,
    string? LastUserSolutionCode
);
