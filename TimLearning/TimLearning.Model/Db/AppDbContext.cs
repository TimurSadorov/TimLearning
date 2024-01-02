using Microsoft.EntityFrameworkCore;
using TimLearning.Model.Entities;

namespace TimLearning.Model.Db;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions options)
        : base(options) { }
}
