using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;

namespace TimLearning.Application.Services.UserSolutionServices;

public class UserSolutionService : IUserSolutionService
{
    private readonly IAppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserSolutionService(IAppDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Create(
        Guid userId,
        Guid exerciseId,
        string code,
        CancellationToken ct = default
    )
    {
        var now = await _dateTimeProvider.GetUtcNow();
        var userSolution = new UserSolution
        {
            UserId = userId,
            ExerciseId = exerciseId,
            Code = code,
            Added = now
        };

        _dbContext.Add(userSolution);
        await _dbContext.SaveChangesAsync(ct);
    }
}
