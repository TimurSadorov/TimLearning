using TimLearning.Shared.Services.Encryptors.PasswordEncryptor;

namespace TimLearning.Application.Services.UserServices;

public class UserPasswordService : IUserPasswordService
{
    private readonly IPasswordEncryptor _passwordEncryptor;

    public UserPasswordService(IPasswordEncryptor passwordEncryptor)
    {
        _passwordEncryptor = passwordEncryptor;
    }

    public HashedPasswordDto GetPasswordHash(string password)
    {
        return _passwordEncryptor.GetHashedPassword(password, saltSize: 16);
    }

    public bool IsValidPassword(
        string verifiablePassword,
        string realPasswordHash,
        string passwordSalt
    )
    {
        var verifiableHash = _passwordEncryptor.ComputeHash(verifiablePassword, passwordSalt);
        return verifiableHash == realPasswordHash;
    }
}
