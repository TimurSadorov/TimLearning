namespace TimLearning.Application.Services.ExerciseServices.Dto;

public record ExerciseTestingResult(ExerciseTestingStatus Status, string? ErrorMessage = null);
