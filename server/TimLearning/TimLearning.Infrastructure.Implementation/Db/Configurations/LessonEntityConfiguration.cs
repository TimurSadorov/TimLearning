using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimLearning.Domain.Entities;

namespace TimLearning.Infrastructure.Implementation.Db.Configurations;

public class LessonEntityConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasIndex(m => m.NextLessonId).IsUnique();
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Lesson_DeletedHasNextLessonWithNull",
                """ "IsDeleted" = false or "IsDeleted" = true and "NextLessonId" is null """
            );
        });
        builder.HasOne(l => l.NextLesson).WithOne(l => l.PreviousLesson);

        builder.HasOne(l => l.Exercise).WithOne().HasForeignKey<Lesson>(l => l.ExerciseId);
    }
}
