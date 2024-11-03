using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class extras2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exstras",
                table: "Assignments");

            migrationBuilder.AddColumn<double>(
                name: "Extras",
                table: "Assignments",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extras",
                table: "Assignments");

            migrationBuilder.AddColumn<double>(
                name: "Exstras",
                table: "Assignments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
