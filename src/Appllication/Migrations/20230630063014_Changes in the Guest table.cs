using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appllication.Migrations
{
    /// <inheritdoc />
    public partial class ChangesintheGuesttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVegetarian",
                table: "Guests",
                newName: "IsComing");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVeggiesMenus",
                table: "Guests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfVeggiesMenus",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "IsComing",
                table: "Guests",
                newName: "IsVegetarian");
        }
    }
}
