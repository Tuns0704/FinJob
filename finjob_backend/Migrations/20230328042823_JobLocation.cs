using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finjob_backend.Migrations
{
    /// <inheritdoc />
    public partial class JobLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Positions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Locations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_JobId",
                table: "Positions",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_JobId",
                table: "Locations",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Jobs_JobId",
                table: "Locations",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Jobs_JobId",
                table: "Positions",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Jobs_JobId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Jobs_JobId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_JobId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Locations_JobId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Locations");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
