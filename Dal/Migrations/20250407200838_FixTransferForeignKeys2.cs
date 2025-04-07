using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class FixTransferForeignKeys2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_AspNetUsers_UserId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_AspNetUsers_UserId1",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_UserId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_UserId1",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Transfers");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_GiverUserId",
                table: "Transfers",
                column: "GiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_TakerUserId",
                table: "Transfers",
                column: "TakerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_AspNetUsers_GiverUserId",
                table: "Transfers",
                column: "GiverUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_AspNetUsers_TakerUserId",
                table: "Transfers",
                column: "TakerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_AspNetUsers_GiverUserId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_AspNetUsers_TakerUserId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_GiverUserId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_TakerUserId",
                table: "Transfers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Transfers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Transfers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_UserId",
                table: "Transfers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_UserId1",
                table: "Transfers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_AspNetUsers_UserId",
                table: "Transfers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_AspNetUsers_UserId1",
                table: "Transfers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
