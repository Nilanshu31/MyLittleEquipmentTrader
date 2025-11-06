using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLittleEquipmentTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManufacturerName",
                table: "Manufacturers",
                newName: "Name1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name1",
                table: "Manufacturers",
                newName: "ManufacturerName");
        }
    }
}
