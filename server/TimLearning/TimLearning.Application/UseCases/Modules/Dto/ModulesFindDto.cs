namespace TimLearning.Application.UseCases.Modules.Dto;

public record ModulesFindDto(Guid CourseId, bool IsDeleted, bool? IsDraft);
