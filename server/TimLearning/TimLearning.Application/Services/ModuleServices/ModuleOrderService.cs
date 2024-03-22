using Microsoft.EntityFrameworkCore;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.Services.ModuleServices;

public class ModuleOrderService : IModuleOrderService
{
    private readonly IAppDbContext _dbContext;

    public ModuleOrderService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetNextOrderAsync(Guid courseId, CancellationToken ct = default)
    {
        var lastOrder = await _dbContext.Modules
            .Where(m => m.CourseId == courseId && m.Order != null)
            .MaxAsync(m => m.Order, ct);

        return lastOrder + 1 ?? 1;
    }
}
