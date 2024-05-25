using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimLearning.Domain.Entities;

namespace TimLearning.Infrastructure.Implementation.Db.Configurations;

public class CodeReviewEntityConfiguration : IEntityTypeConfiguration<CodeReview>
{
    public void Configure(EntityTypeBuilder<CodeReview> builder)
    {
        builder.HasIndex(r => new { r.UserSolutionId, r.GroupStudentId }).IsUnique();
    }
}
