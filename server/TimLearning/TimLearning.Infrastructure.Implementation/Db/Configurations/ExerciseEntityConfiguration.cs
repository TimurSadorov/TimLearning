using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Implementation.Db.Consts;
using TimLearning.Infrastructure.Implementation.Db.Extensions;

namespace TimLearning.Infrastructure.Implementation.Db.Configurations;

public class ExerciseEntityConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasOne(e => e.Lesson).WithMany().HasForeignKey(e => e.LessonId);

        builder
            .Property(c => c.AppContainerData)
            .HasColumnType(PostgresTypes.Json)
            .HasJsonConversion();

        builder
            .Property(c => c.ServiceApps)
            .HasColumnType(PostgresTypes.Json)
            .HasListJsonConversion();
    }
}
