using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimLearning.Infrastructure.Implementation.Db.Migrations
{
    /// <inheritdoc />
    public partial class Add_Study_Group_And_Group_Student_And_Code_Review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_Lessons_ExerciseId", table: "Lessons");

            migrationBuilder.AddColumn<Guid>(
                name: "LessonId",
                table: "Exercises",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.CreateTable(
                name: "StudyGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    MentorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Added = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyGroups_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_StudyGroups_Users_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "GroupStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudyGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Added = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupStudents_StudyGroups_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "StudyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_GroupStudents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "CodeReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupStudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserSolutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Completed = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeReviews_GroupStudents_GroupStudentId",
                        column: x => x.GroupStudentId,
                        principalTable: "GroupStudents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_CodeReviews_UserSolutions_UserSolutionId",
                        column: x => x.UserSolutionId,
                        principalTable: "UserSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ExerciseId",
                table: "Lessons",
                column: "ExerciseId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_LessonId",
                table: "Exercises",
                column: "LessonId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_CodeReviews_GroupStudentId",
                table: "CodeReviews",
                column: "GroupStudentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_CodeReviews_UserSolutionId_GroupStudentId",
                table: "CodeReviews",
                columns: new[] { "UserSolutionId", "GroupStudentId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudents_StudyGroupId",
                table: "GroupStudents",
                column: "StudyGroupId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudents_UserId_StudyGroupId",
                table: "GroupStudents",
                columns: new[] { "UserId", "StudyGroupId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_CourseId",
                table: "StudyGroups",
                column: "CourseId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_MentorId",
                table: "StudyGroups",
                column: "MentorId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Lessons_LessonId",
                table: "Exercises",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Lessons_LessonId",
                table: "Exercises"
            );

            migrationBuilder.DropTable(name: "CodeReviews");

            migrationBuilder.DropTable(name: "GroupStudents");

            migrationBuilder.DropTable(name: "StudyGroups");

            migrationBuilder.DropIndex(name: "IX_Lessons_ExerciseId", table: "Lessons");

            migrationBuilder.DropIndex(name: "IX_Exercises_LessonId", table: "Exercises");

            migrationBuilder.DropColumn(name: "LessonId", table: "Exercises");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ExerciseId",
                table: "Lessons",
                column: "ExerciseId"
            );
        }
    }
}
