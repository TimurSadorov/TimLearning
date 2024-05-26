using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimLearning.Infrastructure.Implementation.Db.Migrations
{
    /// <inheritdoc />
    public partial class Add_Code_Review_Note_And_Comment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeReviewNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodeReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartPosition = table.Column<string>(type: "json", nullable: false),
                    EndPosition = table.Column<string>(type: "json", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Added = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeReviewNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeReviewNotes_CodeReviews_CodeReviewId",
                        column: x => x.CodeReviewId,
                        principalTable: "CodeReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodeReviewNoteComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodeReviewNoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Added = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeReviewNoteComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeReviewNoteComments_CodeReviewNotes_CodeReviewNoteId",
                        column: x => x.CodeReviewNoteId,
                        principalTable: "CodeReviewNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CodeReviewNoteComments_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeReviewNoteComments_AuthorId",
                table: "CodeReviewNoteComments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeReviewNoteComments_CodeReviewNoteId",
                table: "CodeReviewNoteComments",
                column: "CodeReviewNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeReviewNotes_CodeReviewId",
                table: "CodeReviewNotes",
                column: "CodeReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeReviewNoteComments");

            migrationBuilder.DropTable(
                name: "CodeReviewNotes");
        }
    }
}
