namespace TimLearning.Application.UseCases.Courses.Dto;

public record CourseDto(
    Guid Id,
    string Name,
    string ShortName,
    string Description,
    bool IsDraft,
    bool IsDeleted
);
