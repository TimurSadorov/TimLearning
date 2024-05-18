namespace TimLearning.Application.Services.UserProgressServices;

public interface IUserProgressService
{
    Task Complete(Guid lessonId, Guid userId, CancellationToken ct = default);
}
