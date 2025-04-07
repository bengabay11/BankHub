using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class RenameAccountToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TakerAccountId",
                table: "Transfers",
                newName: "TakerUserId");

            migrationBuilder.RenameColumn(
                name: "GiverAccountId",
                table: "Transfers",
                newName: "GiverUserId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "DisplayName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TakerUserId",
                table: "Transfers",
                newName: "TakerAccountId");

            migrationBuilder.RenameColumn(
                name: "GiverUserId",
                table: "Transfers",
                newName: "GiverAccountId");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "AspNetUsers",
                newName: "Name");
        }
    }
}
