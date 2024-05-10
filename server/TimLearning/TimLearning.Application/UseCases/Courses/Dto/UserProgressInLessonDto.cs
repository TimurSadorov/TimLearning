using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Courses.Dto;

public record UserProgressInLessonDto(
    Guid Id,
    string Name,
    bool IsPractical,
    UserProgressType? UserProgress
);
