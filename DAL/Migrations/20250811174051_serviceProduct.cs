using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class serviceProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Assignments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfProducts",
                table: "Assignments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calculation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CalculationAssignment",
                columns: table => new
                {
                    CalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationAssignment", x => new { x.CalculationId, x.AssignmentId });
                    table.ForeignKey(
                        name: "FK_CalculationAssignment_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculationAssignment_Calculation_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculationAssignment_AssignmentId",
                table: "CalculationAssignment",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProducts_ProductId",
                table: "ServiceProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculationAssignment");

            migrationBuilder.DropTable(
                name: "ServiceProducts");

            migrationBuilder.DropTable(
                name: "Calculation");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "NumOfProducts",
                table: "Assignments");
        }
    }
}
