using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeniusContactManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CellNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "CellNo", "CreatedDate", "Email", "ModifiedDate", "Name", "Surname", "Used" },
                values: new object[,]
                {
                    { 1, null, "123-456-7890", new DateTime(2025, 8, 28, 16, 55, 59, 602, DateTimeKind.Local).AddTicks(7482), "john.doe@email.com", null, "John", "Doe", false },
                    { 2, null, "987-654-3210", new DateTime(2025, 8, 28, 16, 55, 59, 602, DateTimeKind.Local).AddTicks(7678), "jane.smith@email.com", null, "Jane", "Smith", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CellNo",
                table: "Contacts",
                column: "CellNo");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Name_Surname",
                table: "Contacts",
                columns: new[] { "Name", "Surname" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
