namespace TimLearning.Application.Services.UserSolutionServices;

public interface IUserSolutionService
{
    Task<Guid> Create(Guid userId, Guid exerciseId, string code, CancellationToken ct = default);
}
