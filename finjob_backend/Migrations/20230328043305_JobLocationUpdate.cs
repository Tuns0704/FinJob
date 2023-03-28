using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finjob_backend.Migrations
{
    /// <inheritdoc />
    public partial class JobLocationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Jobs_JobId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_JobId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Locations");

            migrationBuilder.CreateTable(
                name: "JobLocation",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false),
                    LocationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLocation", x => new { x.JobsId, x.LocationsId });
                    table.ForeignKey(
                        name: "FK_JobLocation_Jobs_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobLocation_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobLocation_LocationsId",
                table: "JobLocation",
                column: "LocationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobLocation");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Locations",
                type: "int",
                nullable: true);

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
        }
    }
}
