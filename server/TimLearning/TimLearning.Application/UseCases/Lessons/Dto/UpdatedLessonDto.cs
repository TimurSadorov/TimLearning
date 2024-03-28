namespace TimLearning.Application.UseCases.Lessons.Dto;

public record UpdatedLessonDto(Guid Id, string? Name, string? Text, bool? IsDraft);
