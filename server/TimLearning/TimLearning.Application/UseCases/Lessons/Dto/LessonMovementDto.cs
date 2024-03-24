namespace TimLearning.Application.UseCases.Lessons.Dto;

public record LessonMovementDto(Guid LessonId, Guid? NextLessonId);
