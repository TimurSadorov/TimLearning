namespace TimLearning.Application.Services.CourseServices.Dto;

public record CourseCreateDto(
    string Name,
    string ShortName,
    string Description,
    bool IsDraft,
    bool IsDeleted
);
