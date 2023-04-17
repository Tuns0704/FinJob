using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finjob_backend.Migrations
{
    /// <inheritdoc />
    public partial class JobAndPostion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Jobs_JobId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_JobId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Positions");

            migrationBuilder.CreateTable(
                name: "JobPosition",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false),
                    PositionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPosition", x => new { x.JobsId, x.PositionsId });
                    table.ForeignKey(
                        name: "FK_JobPosition_Jobs_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPosition_Positions_PositionsId",
                        column: x => x.PositionsId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPosition_PositionsId",
                table: "JobPosition",
                column: "PositionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPosition");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Positions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_JobId",
                table: "Positions",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Jobs_JobId",
                table: "Positions",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}
