namespace TimLearning.Application.UseCases.StudyGroups.Dto;

public record UpdatableStudyGroupDto(Guid Id, string? Name, bool? IsActive);
