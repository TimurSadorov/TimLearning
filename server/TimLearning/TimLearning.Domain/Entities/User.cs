using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class User : IIdHolder<Guid>
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }
    public required bool IsEmailConfirmed { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpireAt { get; set; }
}
