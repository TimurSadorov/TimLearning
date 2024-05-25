namespace TimLearning.Application.UseCases.StudyGroups.Dto;

public record StudyGroupsFindDto(List<Guid>? Ids, string? SearchName, bool? IsActive);
