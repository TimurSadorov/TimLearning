using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimLearning.Domain.Entities;

namespace TimLearning.Infrastructure.Implementation.Db.Configurations;

public class UserProgressEntityConfiguration : IEntityTypeConfiguration<UserProgress>
{
    public void Configure(EntityTypeBuilder<UserProgress> builder)
    {
        builder.HasKey(p => new { p.UserId, p.LessonId });
    }
}
