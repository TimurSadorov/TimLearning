using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Module;

public record ModuleAllDataResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    int? Order,
    [property: Required] Guid CourseId,
    [property: Required] bool IsDraft,
    [property: Required] bool IsDeleted
);
