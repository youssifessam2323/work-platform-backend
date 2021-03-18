using Microsoft.EntityFrameworkCore.Migrations;

namespace work_platform_backend.Migrations
{
    public partial class OnetTwoManyReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ReplyId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ReplyId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "ReplyId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReplyId",
                table: "Comments",
                column: "ReplyId",
                unique: true,
                filter: "[ReplyId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ReplyId",
                table: "Comments",
                column: "ReplyId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
