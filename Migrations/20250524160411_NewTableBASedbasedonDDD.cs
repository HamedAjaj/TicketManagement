using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketManagement.Migrations
{
    /// <inheritdoc />
    public partial class NewTableBASedbasedonDDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Governorate",
                table: "Tickets",
                newName: "Address_Governorate");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Tickets",
                newName: "Address_District");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Tickets",
                newName: "Address_City");

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Governorate",
                table: "Tickets",
                newName: "Governorate");

            migrationBuilder.RenameColumn(
                name: "Address_District",
                table: "Tickets",
                newName: "District");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Tickets",
                newName: "City");

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
