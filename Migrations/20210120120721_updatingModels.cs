using Microsoft.EntityFrameworkCore.Migrations;

namespace work_platform_backend.Migrations
{
    public partial class updatingModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Rooms_RoomId",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_RoomId",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Settings");

            migrationBuilder.AlterColumn<bool>(
                name: "value",
                table: "Settings",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RoomSettings",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    SettingId = table.Column<int>(nullable: false),
                    Setting = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomSettings", x => new { x.RoomId, x.SettingId });
                    table.ForeignKey(
                        name: "FK_RoomSettings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomSettings_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomSettings_SettingId",
                table: "RoomSettings",
                column: "SettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomSettings");

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Settings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settings_RoomId",
                table: "Settings",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Rooms_RoomId",
                table: "Settings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
