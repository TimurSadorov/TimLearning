using TimLearning.Application.Services.ExerciseServices.Dto;

namespace TimLearning.Application.UseCases.Lessons.Dto;

public record LessonUpdatingResultDto(bool IsSuccess, ExerciseTestingResult? TestingResult);
