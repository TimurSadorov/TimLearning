using System.Security.Cryptography;
using System.Text;

namespace TimLearning.Shared.Services.Encryptors.PasswordEncryptor;

public class PasswordEncryptor : IPasswordEncryptor, IDisposable
{
    private const string CharChoicesForSalt =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    private readonly HashAlgorithm _hashAlgorithm;

    public PasswordEncryptor(HashAlgorithm hashAlgorithm)
    {
        _hashAlgorithm = hashAlgorithm;
    }

    public HashedPasswordDto GetHashedPassword(string password, int saltSize)
    {
        var salt = RandomNumberGenerator.GetString(CharChoicesForSalt, saltSize);
        var hash = ComputeHash(password, salt);
        return new HashedPasswordDto(salt, hash);
    }

    public string ComputeHash(string password, string salt)
    {
        var hash = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
        return string.Concat(hash.Select(x => x.ToString("X4")));
    }

    public void Dispose()
    {
        _hashAlgorithm.Dispose();
    }
}
