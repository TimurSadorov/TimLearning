using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.User;

public record RefreshRequest
{
    [Required]
    public required string UserEmail { get; init; }

    [Required]
    public required string RefreshToken { get; init; }
}
