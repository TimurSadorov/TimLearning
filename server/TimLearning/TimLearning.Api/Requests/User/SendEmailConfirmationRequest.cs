using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.User;

public record SendEmailConfirmationRequest
{
    [Required]
    public required string UserEmail { get; init; }
}
