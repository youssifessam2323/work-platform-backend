using Microsoft.EntityFrameworkCore.Migrations;

namespace work_platform_backend.Migrations
{
    public partial class fixDependantTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RTaskId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_RTaskId",
                table: "Tasks",
                column: "RTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tasks_RTaskId",
                table: "Tasks",
                column: "RTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_RTaskId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_RTaskId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RTaskId",
                table: "Tasks");
        }
    }
}
