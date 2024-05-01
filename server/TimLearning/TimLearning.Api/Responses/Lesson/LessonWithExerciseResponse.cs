using System.ComponentModel.DataAnnotations;
using TimLearning.Api.Responses.Exercise;

namespace TimLearning.Api.Responses.Lesson;

public record LessonWithExerciseResponse(
    [property: Required] string Name,
    [property: Required] string Text,
    ExerciseResponse? Exercise
);
