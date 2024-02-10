using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.User;

public record SendMailToRecoverPasswordRequest
{
    [Required]
    public required string UserEmail { get; init; }
}
