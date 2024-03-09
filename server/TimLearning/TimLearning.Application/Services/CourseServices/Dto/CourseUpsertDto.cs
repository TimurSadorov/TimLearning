namespace TimLearning.Application.Services.CourseServices.Dto;

public record CourseUpsertDto(
    Guid Id,
    string? Name,
    string? ShortName,
    string? Description,
    bool? IsDraft,
    bool? IsDeleted
);
