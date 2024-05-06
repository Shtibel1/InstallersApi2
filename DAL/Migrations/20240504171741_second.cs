using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallerPricing_Worker_InstallerId1",
                table: "InstallerPricing");

            migrationBuilder.DropIndex(
                name: "IX_InstallerPricing_InstallerId1",
                table: "InstallerPricing");

            migrationBuilder.DropColumn(
                name: "InstallerId1",
                table: "InstallerPricing");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstallerId",
                table: "InstallerPricing",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_InstallerPricing_InstallerId",
                table: "InstallerPricing",
                column: "InstallerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallerPricing_Worker_InstallerId",
                table: "InstallerPricing",
                column: "InstallerId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallerPricing_Worker_InstallerId",
                table: "InstallerPricing");

            migrationBuilder.DropIndex(
                name: "IX_InstallerPricing_InstallerId",
                table: "InstallerPricing");

            migrationBuilder.AlterColumn<string>(
                name: "InstallerId",
                table: "InstallerPricing",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "InstallerId1",
                table: "InstallerPricing",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InstallerPricing_InstallerId1",
                table: "InstallerPricing",
                column: "InstallerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallerPricing_Worker_InstallerId1",
                table: "InstallerPricing",
                column: "InstallerId1",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
