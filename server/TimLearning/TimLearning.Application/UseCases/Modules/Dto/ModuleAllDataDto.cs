namespace TimLearning.Application.UseCases.Modules.Dto;

public record ModuleAllDataDto(
    Guid Id,
    string Name,
    int? Order,
    Guid CourseId,
    bool IsDraft,
    bool IsDeleted
);
