using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.User;

public record RecoverPasswordRequest
{
    [Required]
    public required string UserEmail { get; init; }
}
