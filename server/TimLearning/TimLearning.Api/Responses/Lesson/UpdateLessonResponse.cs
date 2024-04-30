using System.ComponentModel.DataAnnotations;
using TimLearning.Api.Responses.Exercise;

namespace TimLearning.Api.Responses.Lesson;

public record UpdateLessonResponse(
    [property: Required] bool IsSuccess,
    ExerciseTestingResultResponse? TestingResult
);
