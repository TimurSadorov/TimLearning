using Microsoft.EntityFrameworkCore;

namespace TimLearning.Model.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options)
        : base(options) { }
}
