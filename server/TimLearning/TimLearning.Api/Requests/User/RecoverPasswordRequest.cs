using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.User;

public record RecoverPasswordRequest
{
    [Required]
    public required string UserEmail { get; init; }
    
    [Required]
    public required string NewPassword { get; init; }
    
    [Required]
    public required string Signature { get; init; }
}
