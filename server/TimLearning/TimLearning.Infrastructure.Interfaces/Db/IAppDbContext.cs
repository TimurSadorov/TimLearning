using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TimLearning.Domain.Entities;

namespace TimLearning.Infrastructure.Interfaces.Db;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<Course> Courses { get; }
    DbSet<Module> Modules { get; }
    DbSet<Lesson> Lessons { get; }
    DbSet<Exercise> Exercises { get; }
    DbSet<StoredFile> StoredFiles { get; }
    DbSet<UserProgress> UserProgresses { get; }
    DbSet<UserSolution> UserSolutions { get; }
    DbSet<StudyGroup> StudyGroups { get; }
    DbSet<GroupStudent> GroupStudents { get; }
    DbSet<CodeReview> CodeReviews { get; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        where TEntity : class;

    void AddRange(IEnumerable<object> entity);

    EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        where TEntity : class;
}
