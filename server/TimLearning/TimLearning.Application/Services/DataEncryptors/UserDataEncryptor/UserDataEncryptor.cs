using TimLearning.Shared.Services.Encryptors.DataEncryptor;

namespace TimLearning.Application.Services.DataEncryptors.UserDataEncryptor;

public class UserDataEncryptor : IUserDataEncryptor
{
    private readonly IDataEncryptor _dataEncryptor;

    public UserDataEncryptor(IDataEncryptor dataEncryptor)
    {
        _dataEncryptor = dataEncryptor;
    }

    public string SingEmail(string email)
    {
        return _dataEncryptor.Sign(email);
    }

    public bool VerifyEmail(string email, string signature)
    {
        return _dataEncryptor.VerifySignature(signature, email);
    }
}
