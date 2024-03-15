namespace TimLearning.Application.UseCases.Courses.Dto;

public record CourseCreateDto(
    string Name,
    string ShortName,
    string Description,
    bool IsDraft,
    bool IsDeleted
);
