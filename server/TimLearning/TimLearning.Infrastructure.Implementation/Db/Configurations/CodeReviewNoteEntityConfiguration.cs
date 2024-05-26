using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Implementation.Db.Consts;
using TimLearning.Infrastructure.Implementation.Db.Extensions;

namespace TimLearning.Infrastructure.Implementation.Db.Configurations;

public class CodeReviewNoteEntityConfiguration : IEntityTypeConfiguration<CodeReviewNote>
{
    public void Configure(EntityTypeBuilder<CodeReviewNote> builder)
    {
        builder
            .Property(r => r.StartPosition)
            .HasColumnType(PostgresTypes.Json)
            .HasJsonConversion();

        builder.Property(r => r.EndPosition).HasColumnType(PostgresTypes.Json).HasJsonConversion();
    }
}
