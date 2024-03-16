using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Module;

public class FindOrderedModulesRequest
{
    [Required]
    public required bool IsDeleted { get; init; }
    public bool? IsDraft { get; init; }
}
