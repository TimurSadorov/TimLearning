using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Module;

public record FindOrderedModulesResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    [property: Required] bool IsDraft,
    [property: Required] bool IsDeleted
);
