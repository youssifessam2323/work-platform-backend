using Microsoft.EntityFrameworkCore.Migrations;

namespace work_platform_backend.Migrations
{
    public partial class compositeteammembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamsMembers_Teams_teamId",
                table: "TeamsMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamsMembers_AspNetUsers_userId",
                table: "TeamsMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamsMembers",
                table: "TeamsMembers");

            migrationBuilder.DropIndex(
                name: "IX_TeamsMembers_userId",
                table: "TeamsMembers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeamsMembers");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "TeamsMembers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "teamId",
                table: "TeamsMembers",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamsMembers_teamId",
                table: "TeamsMembers",
                newName: "IX_TeamsMembers_TeamId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TeamsMembers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "TeamsMembers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamsMembers",
                table: "TeamsMembers",
                columns: new[] { "UserId", "TeamId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsMembers_Teams_TeamId",
                table: "TeamsMembers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsMembers_AspNetUsers_UserId",
                table: "TeamsMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamsMembers_Teams_TeamId",
                table: "TeamsMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamsMembers_AspNetUsers_UserId",
                table: "TeamsMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamsMembers",
                table: "TeamsMembers");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TeamsMembers",
                newName: "teamId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TeamsMembers",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamsMembers_TeamId",
                table: "TeamsMembers",
                newName: "IX_TeamsMembers_teamId");

            migrationBuilder.AlterColumn<int>(
                name: "teamId",
                table: "TeamsMembers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "TeamsMembers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TeamsMembers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamsMembers",
                table: "TeamsMembers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsMembers_userId",
                table: "TeamsMembers",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsMembers_Teams_teamId",
                table: "TeamsMembers",
                column: "teamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsMembers_AspNetUsers_userId",
                table: "TeamsMembers",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
