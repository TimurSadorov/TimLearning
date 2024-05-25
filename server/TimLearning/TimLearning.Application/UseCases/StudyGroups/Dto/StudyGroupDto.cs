namespace TimLearning.Application.UseCases.StudyGroups.Dto;

public record StudyGroupDto(Guid Id, string Name, bool IsActive, Guid CourseId);
