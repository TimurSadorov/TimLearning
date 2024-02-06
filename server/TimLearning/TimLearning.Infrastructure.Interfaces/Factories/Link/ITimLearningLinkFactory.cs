namespace TimLearning.Infrastructure.Interfaces.Factories.Link;

public interface ITimLearningLinkFactory
{
    string GetLinkToUserConfirm(string userEmail, string signature);
}
