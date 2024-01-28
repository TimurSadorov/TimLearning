namespace TimLearning.Application.Services.DataEncryptors.PasswordEncryptor;

public record HashedPasswordDto(string Salt, string HashWithSalt);
