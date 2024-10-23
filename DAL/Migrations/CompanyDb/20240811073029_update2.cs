using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Additionals_Assignments_AssignmentId",
                table: "Additionals");

            migrationBuilder.DropIndex(
                name: "IX_Additionals_AssignmentId",
                table: "Additionals");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Additionals");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "AssignmentId",
                table: "Additionals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Additionals_AssignmentId",
                table: "Additionals",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Additionals_Assignments_AssignmentId",
                table: "Additionals",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id");
        }
    }
}
