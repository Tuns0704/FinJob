using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finjob_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFKToCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyID",
                table: "Jobs",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Companies_CompanyID",
                table: "Jobs",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Companies_CompanyID",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyID",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Jobs");
        }
    }
}
