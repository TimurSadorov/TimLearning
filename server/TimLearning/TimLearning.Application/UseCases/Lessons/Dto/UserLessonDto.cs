namespace TimLearning.Application.UseCases.Lessons.Dto;

public record UserLessonDto(
    Guid Id,
    string Name,
    string Text,
    Guid CourseId,
    UserSolutionDto? UserSolution
);
