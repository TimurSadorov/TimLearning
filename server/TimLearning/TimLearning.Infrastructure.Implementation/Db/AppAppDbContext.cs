using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Infrastructure.Implementation.Db;

public class AppAppDbContext : DbContext, IAppDbContext
{
    public DbSet<User> Users => Set<User>();

    public AppAppDbContext(DbContextOptions options)
        : base(options) { }
}
