using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halisaha.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ReservedSessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Transport",
                table: "Owners",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ReservedSessions");

            migrationBuilder.DropColumn(
                name: "Transport",
                table: "Owners");
        }
    }
}
