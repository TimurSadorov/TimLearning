using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.User;

public record UserEmailConfirmationRequest
{
    [Required]
    public required string Email { get; init; }

    [Required]
    public required string Signature { get; init; }
}
