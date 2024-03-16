using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimLearning.Domain.Entities;

namespace TimLearning.Infrastructure.Implementation.Db.Configurations;

public class ModuleEntityConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.HasIndex(m => new { m.CourseId, m.Order }).IsUnique();
        builder.ToTable(t => t.HasCheckConstraint("CK_Modules_NotNegativeOrder", "\"Order\" > 0"));
        builder.ToTable(
            t =>
                t.HasCheckConstraint(
                    "CK_Modules_OrderHasValue",
                    "(\"IsDeleted\" = false and \"Order\" is not null) or (\"IsDeleted\" = true and \"Order\" is null)"
                )
        );
    }
}
