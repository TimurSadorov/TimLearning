namespace TimLearning.Application.UseCases.Modules.Dto;

public record OrderedModuleDto(Guid Id, string Name, bool IsDraft, bool IsDeleted);
