using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class FixTransferForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
