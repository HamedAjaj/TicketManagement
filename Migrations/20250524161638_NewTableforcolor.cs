using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketManagement.Migrations
{
    /// <inheritdoc />
    public partial class NewTableforcolor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "Tickets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
