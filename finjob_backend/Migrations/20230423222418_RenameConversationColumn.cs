using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finjob_backend.Migrations
{
    /// <inheritdoc />
    public partial class RenameConversationColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "Conversations",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "Receiver",
                table: "Conversations",
                newName: "ReceiverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Conversations",
                newName: "Sender");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Conversations",
                newName: "Receiver");
        }
    }
}
