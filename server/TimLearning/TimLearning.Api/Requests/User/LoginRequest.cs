using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.User;

public record LoginRequest
{
    [Required]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }
}
