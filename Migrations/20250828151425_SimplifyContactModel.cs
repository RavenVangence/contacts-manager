using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeniusContactManager.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyContactModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "CellNo",
                table: "Contacts",
                newName: "PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_CellNo",
                table: "Contacts",
                newName: "IX_Contacts_PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Contacts",
                newName: "CellNo");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_PhoneNumber",
                table: "Contacts",
                newName: "IX_Contacts_CellNo");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Email" },
                values: new object[] { null, "john.doe@email.com" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Email" },
                values: new object[] { null, "jane.smith@email.com" });
        }
    }
}
