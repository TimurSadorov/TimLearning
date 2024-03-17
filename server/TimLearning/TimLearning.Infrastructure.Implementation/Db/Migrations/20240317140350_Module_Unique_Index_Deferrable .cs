using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimLearning.Infrastructure.Implementation.Db.Migrations
{
    /// <inheritdoc />
    public partial class Module_Unique_Index_Deferrable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DROP INDEX "IX_Modules_CourseId_Order";
                ALTER TABLE "Modules"
                ADD CONSTRAINT "IX_Modules_CourseId_Order"
                    UNIQUE ("CourseId", "Order") DEFERRABLE INITIALLY DEFERRED;
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                ALTER TABLE "Modules"
                    DROP CONSTRAINT "IX_Modules_CourseId_Order";
                CREATE UNIQUE INDEX "IX_Modules_CourseId_Order" ON "Modules" ("CourseId", "Order");
                """
            );
        }
    }
}
