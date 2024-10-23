using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class assignmentPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Additionals_Products_ProductId",
                table: "Additionals");

            migrationBuilder.DropIndex(
                name: "IX_Additionals_ProductId",
                table: "Additionals");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Additionals");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Additionals");

            migrationBuilder.DropColumn(
                name: "ServiceProviderIdExt",
                table: "Additionals");

            migrationBuilder.CreateTable(
                name: "AdditionalsPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceProviderIdExt = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalsPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalsPrices_Additionals_AdditionalId",
                        column: x => x.AdditionalId,
                        principalTable: "Additionals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdditionalsPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalsPrices_AdditionalId",
                table: "AdditionalsPrices",
                column: "AdditionalId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalsPrices_ProductId",
                table: "AdditionalsPrices",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalsPrices");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Additionals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Additionals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceProviderIdExt",
                table: "Additionals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Additionals_ProductId",
                table: "Additionals",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Additionals_Products_ProductId",
                table: "Additionals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
