using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Module;

public class CreateModuleRequest
{
    [Required]
    public required string Name { get; init; }
}
