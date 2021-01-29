﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace work_platform_backend.Migrations
{
    public partial class AddCreatorOfRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CreatorId",
                table: "Rooms",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_CreatorId",
                table: "Rooms",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_CreatorId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_CreatorId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Rooms");
        }
    }
}
