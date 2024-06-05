using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimLearning.Infrastructure.Implementation.Db.Migrations
{
    /// <inheritdoc />
    public partial class Add_User_Solution_Code_Review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CodeReviews_UserSolutionId",
                table: "CodeReviews",
                column: "UserSolutionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CodeReviews_UserSolutionId",
                table: "CodeReviews");
        }
    }
}
