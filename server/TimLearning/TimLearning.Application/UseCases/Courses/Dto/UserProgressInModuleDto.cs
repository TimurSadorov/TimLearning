namespace TimLearning.Application.UseCases.Courses.Dto;

public record UserProgressInModuleDto(
    Guid Id,
    string Name,
    int CompletionPercentage,
    List<UserProgressInLessonDto> Lessons
);
