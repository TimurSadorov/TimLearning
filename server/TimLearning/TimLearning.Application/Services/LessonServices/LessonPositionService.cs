using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.Services.LessonServices;

public class LessonPositionService : ILessonPositionService
{
    private readonly IAppDbContext _dbContext;

    public LessonPositionService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Lesson?> FindLastLesson(Guid moduleId, CancellationToken ct = default)
    {
        return _dbContext.Lessons
            .Where(LessonSpecifications.BeforeLesson(moduleId, null))
            .SingleOrDefaultAsync(ct);
    }
}
