using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TimLearning.Domain.Entities;

namespace TimLearning.Infrastructure.Interfaces.Db;

public interface IAppDbContext
{
    DbSet<User> Users { get; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        where TEntity : class;
}
