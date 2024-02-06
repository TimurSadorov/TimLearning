using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Implementation.Db.Configurations;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Infrastructure.Implementation.Db;

public class AppAppDbContext : DbContext, IAppDbContext
{
    public DbSet<User> Users => Set<User>();

    public AppAppDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfiguration).Assembly);
    }
}
