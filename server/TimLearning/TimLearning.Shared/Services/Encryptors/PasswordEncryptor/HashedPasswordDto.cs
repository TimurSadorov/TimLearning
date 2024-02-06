namespace TimLearning.Shared.Services.Encryptors.PasswordEncryptor;

public record HashedPasswordDto(string Salt, string HashWithSalt);
