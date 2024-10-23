using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class assignmentaddtionals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalsPrices_Assignments_AssignmentId",
                table: "AdditionalsPrices");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalsPrices_AssignmentId",
                table: "AdditionalsPrices");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "AdditionalsPrices");

            migrationBuilder.CreateTable(
                name: "AssignmentAdditionalPrices",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalPriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentAdditionalPrices", x => new { x.AssignmentId, x.AdditionalPriceId });
                    table.ForeignKey(
                        name: "FK_AssignmentAdditionalPrices_AdditionalsPrices_AdditionalPriceId",
                        column: x => x.AdditionalPriceId,
                        principalTable: "AdditionalsPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentAdditionalPrices_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAdditionalPrices_AdditionalPriceId",
                table: "AssignmentAdditionalPrices",
                column: "AdditionalPriceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentAdditionalPrices");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignmentId",
                table: "AdditionalsPrices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalsPrices_AssignmentId",
                table: "AdditionalsPrices",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalsPrices_Assignments_AssignmentId",
                table: "AdditionalsPrices",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id");
        }
    }
}
