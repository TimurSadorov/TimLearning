namespace TimLearning.Application.UseCases.Lessons.Dto;

public record OrderedLessonFindDto(Guid ModuleId, bool? IsDraft);
