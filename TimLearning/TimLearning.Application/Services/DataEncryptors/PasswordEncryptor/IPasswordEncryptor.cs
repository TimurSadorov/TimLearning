namespace TimLearning.Application.Services.DataEncryptors.PasswordEncryptor;

public interface IPasswordEncryptor
{
    HashedPasswordDto GetHashedPassword(string password, int saltSize);

    string ComputeHash(string password, string salt);
}