using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appllication.Migrations
{
    /// <inheritdoc />
    public partial class Changetypeofmenus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfVeggiesMenus",
                table: "Guests");

            migrationBuilder.AddColumn<string>(
                name: "TypesOfMenu",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypesOfMenu",
                table: "Guests");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVeggiesMenus",
                table: "Guests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
