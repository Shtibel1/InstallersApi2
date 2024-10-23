using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Additional_Assignments_AssignmentId",
                table: "Additional");

            migrationBuilder.DropForeignKey(
                name: "FK_Additional_Products_ProductId",
                table: "Additional");

            migrationBuilder.DropTable(
                name: "ServiceProviderPricing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Additional",
                table: "Additional");

            migrationBuilder.RenameTable(
                name: "Additional",
                newName: "Additionals");

            migrationBuilder.RenameIndex(
                name: "IX_Additional_ProductId",
                table: "Additionals",
                newName: "IX_Additionals_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Additional_AssignmentId",
                table: "Additionals",
                newName: "IX_Additionals_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Additionals",
                table: "Additionals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Additionals_Assignments_AssignmentId",
                table: "Additionals",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Additionals_Products_ProductId",
                table: "Additionals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Additionals_Assignments_AssignmentId",
                table: "Additionals");

            migrationBuilder.DropForeignKey(
                name: "FK_Additionals_Products_ProductId",
                table: "Additionals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Additionals",
                table: "Additionals");

            migrationBuilder.RenameTable(
                name: "Additionals",
                newName: "Additional");

            migrationBuilder.RenameIndex(
                name: "IX_Additionals_ProductId",
                table: "Additional",
                newName: "IX_Additional_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Additionals_AssignmentId",
                table: "Additional",
                newName: "IX_Additional_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Additional",
                table: "Additional",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ServiceProviderPricing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarryPrice = table.Column<double>(type: "float", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryOnlyPrice = table.Column<double>(type: "float", nullable: true),
                    DistancePrice = table.Column<double>(type: "float", nullable: true),
                    InnerFloorPrice = table.Column<double>(type: "float", nullable: true),
                    InstallationPrice = table.Column<double>(type: "float", nullable: false),
                    InstallerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OuterFloorPrice = table.Column<double>(type: "float", nullable: true),
                    ServiceProviderIdExternal = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProviderPricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceProviderPricing_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviderPricing_ProductId",
                table: "ServiceProviderPricing",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Additional_Assignments_AssignmentId",
                table: "Additional",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Additional_Products_ProductId",
                table: "Additional",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
