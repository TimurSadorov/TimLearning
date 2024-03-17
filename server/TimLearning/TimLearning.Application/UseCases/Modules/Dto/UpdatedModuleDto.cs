namespace TimLearning.Application.UseCases.Modules.Dto;

public record UpdatedModuleDto(Guid Id, string? Name, bool? IsDraft, int? Order, bool? IsDeleted);
