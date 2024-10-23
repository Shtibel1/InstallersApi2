using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentPrice",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "CarryPrice",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "DistancePrice",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "InnerFloorPrice",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "OuterFloorPrice",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "ServiceProviderId",
                table: "Assignments",
                newName: "ServiceProviderIdExt");

            migrationBuilder.CreateTable(
                name: "Additional",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceProviderIdExt = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Additional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Additional_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Additional_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Additional_AssignmentId",
                table: "Additional",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Additional_ProductId",
                table: "Additional",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Additional");

            migrationBuilder.RenameColumn(
                name: "ServiceProviderIdExt",
                table: "Assignments",
                newName: "ServiceProviderId");

            migrationBuilder.AddColumn<double>(
                name: "AssignmentPrice",
                table: "Assignments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CarryPrice",
                table: "Assignments",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DistancePrice",
                table: "Assignments",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InnerFloorPrice",
                table: "Assignments",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OuterFloorPrice",
                table: "Assignments",
                type: "float",
                nullable: true);
        }
    }
}
