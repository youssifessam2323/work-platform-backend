using Microsoft.EntityFrameworkCore.Migrations;

namespace work_platform_backend.Migrations
{
    public partial class tryFixChildCheckpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentRTaskId",
                table: "CheckPoints",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CheckPoints_ParentRTaskId",
                table: "CheckPoints",
                column: "ParentRTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckPoints_Tasks_ParentRTaskId",
                table: "CheckPoints",
                column: "ParentRTaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckPoints_Tasks_ParentRTaskId",
                table: "CheckPoints");

            migrationBuilder.DropIndex(
                name: "IX_CheckPoints_ParentRTaskId",
                table: "CheckPoints");

            migrationBuilder.DropColumn(
                name: "ParentRTaskId",
                table: "CheckPoints");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
