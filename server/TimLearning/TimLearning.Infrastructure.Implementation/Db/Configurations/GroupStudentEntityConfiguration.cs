using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimLearning.Domain.Entities;

namespace TimLearning.Infrastructure.Implementation.Db.Configurations;

public class GroupStudentEntityConfiguration : IEntityTypeConfiguration<GroupStudent>
{
    public void Configure(EntityTypeBuilder<GroupStudent> builder)
    {
        builder.HasIndex(s => new { StudentId = s.UserId, s.StudyGroupId }).IsUnique();
    }
}
