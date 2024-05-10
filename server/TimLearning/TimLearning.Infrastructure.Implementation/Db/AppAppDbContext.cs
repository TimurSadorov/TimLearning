using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Implementation.Db.Configurations;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Infrastructure.Implementation.Db;

public class AppAppDbContext : DbContext, IAppDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Module> Modules => Set<Module>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<StoredFile> StoredFiles => Set<StoredFile>();
    public DbSet<UserProgress> UserProgresses => Set<UserProgress>();
    public DbSet<UserSolution> UserSolutions => Set<UserSolution>();

    public AppAppDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfiguration).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<Enum>().HaveConversion<string>();
    }
}
