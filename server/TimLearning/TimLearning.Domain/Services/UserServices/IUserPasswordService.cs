using TimLearning.Shared.Services.Encryptors.PasswordEncryptor;

namespace TimLearning.Domain.Services.UserServices;

public interface IUserPasswordService
{
    HashedPasswordDto GetPasswordHash(string password);

    bool IsValidPassword(
        string verifiablePassword,
        string realPasswordHash,
        string passwordSalt
    );
}
