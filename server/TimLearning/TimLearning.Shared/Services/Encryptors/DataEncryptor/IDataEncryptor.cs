namespace TimLearning.Shared.Services.Encryptors.DataEncryptor;

public interface IDataEncryptor
{
    string Sign(object data);

    bool VerifySignature(string signature, object data);
}
