using TimLearning.Application.Services.ExerciseServices.Dto;

namespace TimLearning.Application.UseCases.Lessons.Dto;

public record LessonWithExerciseDto(string Name, string Text, ExerciseDto? Exercise);
