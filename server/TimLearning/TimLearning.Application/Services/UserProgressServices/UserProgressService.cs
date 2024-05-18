using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.Services.UserProgressServices;

public class UserProgressService : IUserProgressService
{
    private readonly IAppDbContext _dbContext;

    public UserProgressService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Complete(Guid lessonId, Guid userId, CancellationToken ct = default)
    {
        var userProgress = await _dbContext
            .UserProgresses.Where(p => p.UserId == userId && p.LessonId == lessonId)
            .FirstOrDefaultAsync(ct);
        if (userProgress is null)
        {
            userProgress = new UserProgress { UserId = userId, LessonId = lessonId };
            _dbContext.Add(userProgress);
        }

        userProgress.Complete();

        await _dbContext.SaveChangesAsync(ct);
    }
}
