using TimLearning.Shared.Services.Encryptors.DataEncryptor;

namespace TimLearning.Application.Services.StudyGroupServices;

public class StudyGroupDataEncryptor : IStudyGroupDataEncryptor
{
    private readonly IDataEncryptor _dataEncryptor;

    public StudyGroupDataEncryptor(IDataEncryptor dataEncryptor)
    {
        _dataEncryptor = dataEncryptor;
    }

    public string SingId(Guid id)
    {
        return _dataEncryptor.Sign(id);
    }

    public bool VerifyId(Guid id, string signature)
    {
        return _dataEncryptor.VerifySignature(signature, id);
    }
}
