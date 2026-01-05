using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportBookFSD.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverVerificationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerificationRemarks",
                table: "DriverProfiles",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VerificationStatus",
                table: "DriverProfiles",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedAt",
                table: "DriverProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifiedByUserId",
                table: "DriverProfiles",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationRemarks",
                table: "DriverProfiles");

            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                table: "DriverProfiles");

            migrationBuilder.DropColumn(
                name: "VerifiedAt",
                table: "DriverProfiles");

            migrationBuilder.DropColumn(
                name: "VerifiedByUserId",
                table: "DriverProfiles");
        }
    }
}
