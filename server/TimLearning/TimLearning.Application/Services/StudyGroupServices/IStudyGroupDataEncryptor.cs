namespace TimLearning.Application.Services.StudyGroupServices;

public interface IStudyGroupDataEncryptor
{
    string SingId(Guid id);

    bool VerifyId(Guid id, string signature);
}
