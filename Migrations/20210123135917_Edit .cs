using Microsoft.EntityFrameworkCore.Migrations;

namespace work_platform_backend.Migrations
{
    public partial class Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_CreatorId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Setting",
                table: "RoomSettings");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_CreatorId",
                table: "Teams",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_CreatorId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "Setting",
                table: "RoomSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_CreatorId",
                table: "Teams",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
