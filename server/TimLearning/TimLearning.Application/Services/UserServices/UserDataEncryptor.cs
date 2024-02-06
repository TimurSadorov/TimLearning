using TimLearning.Shared.Services.Encryptors.DataEncryptor;

namespace TimLearning.Application.Services.UserServices;

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

    public bool VerifyEmail(string signature, string email)
    {
        return _dataEncryptor.VerifySignature(signature, email);
    }

    public string SingEmailAndPassword(string email, string hashedPassword)
    {
        return _dataEncryptor.Sign(GetEmailAndPasswordSignedObject(email, hashedPassword));
    }

    public bool VerifyEmailAndPassword(string signature, string email, string hashedPassword)
    {
        return _dataEncryptor.VerifySignature(
            signature,
            GetEmailAndPasswordSignedObject(email, hashedPassword)
        );
    }

    private static string GetEmailAndPasswordSignedObject(string email, string hashedPassword)
    {
        return email + hashedPassword;
    }
}
