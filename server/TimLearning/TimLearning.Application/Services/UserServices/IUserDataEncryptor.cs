namespace TimLearning.Application.Services.UserServices;

public interface IUserDataEncryptor
{
    string SingEmail(string email);

    public bool VerifyEmail(string signature, string email);

    public string SingEmailAndPassword(string email, string hashedPassword);

    public bool VerifyEmailAndPassword(string signature, string email, string hashedPassword);
}
