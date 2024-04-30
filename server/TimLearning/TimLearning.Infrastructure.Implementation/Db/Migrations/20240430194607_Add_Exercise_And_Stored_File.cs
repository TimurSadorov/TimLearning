using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimLearning.Infrastructure.Implementation.Db.Migrations
{
    /// <inheritdoc />
    public partial class Add_Exercise_And_Stored_File : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "Lessons",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoredFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Added = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AddedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoredFiles_Users_AddedById",
                        column: x => x.AddedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppArchiveId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppContainerData = table.Column<string>(type: "json", nullable: false),
                    RelativePathToDockerfile = table.Column<string>(type: "text", nullable: false),
                    RelativePathToInsertCode = table.Column<string[]>(type: "text[]", nullable: false),
                    StandardCode = table.Column<string>(type: "text", nullable: false),
                    ServiceApps = table.Column<string>(type: "json", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_StoredFiles_AppArchiveId",
                        column: x => x.AppArchiveId,
                        principalTable: "StoredFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ExerciseId",
                table: "Lessons",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_AppArchiveId",
                table: "Exercises",
                column: "AppArchiveId");

            migrationBuilder.CreateIndex(
                name: "IX_StoredFiles_AddedById",
                table: "StoredFiles",
                column: "AddedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Exercises_ExerciseId",
                table: "Lessons",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Exercises_ExerciseId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "StoredFiles");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ExerciseId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "Lessons");
        }
    }
}
