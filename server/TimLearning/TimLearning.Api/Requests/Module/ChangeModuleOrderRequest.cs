using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Module;

public class ChangeModuleOrderRequest
{
    [Required]
    public required int Order { get; init; }
}
