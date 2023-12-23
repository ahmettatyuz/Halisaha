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
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTeam_Team_TeamsId",
                table: "PlayerTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedSessions_Team_TeamId",
                table: "ReservedSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team",
                table: "Team");

            migrationBuilder.RenameTable(
                name: "Team",
                newName: "Teams");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTeam_Teams_TeamsId",
                table: "PlayerTeam",
                column: "TeamsId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedSessions_Teams_TeamId",
                table: "ReservedSessions",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTeam_Teams_TeamsId",
                table: "PlayerTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedSessions_Teams_TeamId",
                table: "ReservedSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Team");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team",
                table: "Team",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTeam_Team_TeamsId",
                table: "PlayerTeam",
                column: "TeamsId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedSessions_Team_TeamId",
                table: "ReservedSessions",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
