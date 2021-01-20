using Microsoft.EntityFrameworkCore.Migrations;

namespace work_platform_backend.Migrations
{
    public partial class updatingModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Rooms_RoomId1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_RoomId1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_RoomId",
                table: "Teams",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Rooms_RoomId",
                table: "Teams",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Rooms_RoomId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_RoomId",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_RoomId1",
                table: "Teams",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Rooms_RoomId1",
                table: "Teams",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
