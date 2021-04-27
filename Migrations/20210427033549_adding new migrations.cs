using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace work_platform_backend.Migrations
{
    public partial class addingnewmigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Teams_TeamId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "ParentTeamId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatMessageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamChats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatName = table.Column<string>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamChats_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamChats_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    ChatId = table.Column<int>(nullable: false),
                    MessageTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_TeamChats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "TeamChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatMessageTypes_MessageTypeId",
                        column: x => x.MessageTypeId,
                        principalTable: "ChatMessageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ParentTeamId",
                table: "Teams",
                column: "ParentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_CreatorId",
                table: "ChatMessages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_MessageTypeId",
                table: "ChatMessages",
                column: "MessageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamChats_CreatorId",
                table: "TeamChats",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamChats_TeamId",
                table: "TeamChats",
                column: "TeamId",
                unique: true,
                filter: "[TeamId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Teams_ParentTeamId",
                table: "Teams",
                column: "ParentTeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Teams_ParentTeamId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "TeamChats");

            migrationBuilder.DropTable(
                name: "ChatMessageTypes");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ParentTeamId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ParentTeamId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamId",
                table: "Teams",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Teams_TeamId",
                table: "Teams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
