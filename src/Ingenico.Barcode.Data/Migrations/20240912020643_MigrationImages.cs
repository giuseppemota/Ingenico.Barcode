using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ingenico.Barcode.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Produto");
        }
    }
}
