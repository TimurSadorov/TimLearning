namespace TimLearning.Shared.Services.Encryptors.PasswordEncryptor;

public interface IPasswordEncryptor
{
    HashedPasswordDto GetHashedPassword(string password, int saltSize);

    string ComputeHash(string password, string salt);
}