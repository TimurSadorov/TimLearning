namespace TimLearning.Application.Services.DataEncryptors.UserDataEncryptor;

public interface IUserDataEncryptor
{
    string SingEmail(string email);

    bool VerifyEmail(string email, string signature);
}
